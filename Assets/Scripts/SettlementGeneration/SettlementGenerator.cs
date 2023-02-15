using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using Terrain;
using UI;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class SettlementGenerator : MonoBehaviour
{
	[SerializeField] public AnimationCurve radiusCurve;

	[SerializeField][Min(1)] public float regionExtent = 2;
	//[SerializeField][Min(0.05f)] public float minBuildingRadius = 1;
	//	[SerializeField][Min(0.25f)] public float maxBuildingRadius = 1.5f;
	[SerializeField] public int rejectionSamples;
	[SerializeField] public int maxPoints;
	[SerializeField] public GameObject[] buildings;

	private PoissonCell[,] poissonCells;
	private List<PoissonPoint> spawnedPoints = new List<PoissonPoint>();

	private List<PoissonPoint> unusedStarts => spawnedPoints.Where(x => !x.usedForStart).ToList();

	public float minBuildingRadius => radiusCurve.Evaluate(0);

	private Vector2 regionSize;
	private Vector3 offset;

	private void Awake()
	{
		GenerateGrid();
		StartCoroutine(Temp());
	}

	private IEnumerator Temp()
	{
		while (true)
		{
			yield return new WaitForSeconds(0.5f);
			//	radiusCurve.Evaluate(Random.value);
			AddnextBuildings();
		}

	}

	private void OverrideBuilding()
	{


	}


	private void AddnextBuildings()
	{
		List<PoissonPoint> points = CreatePointSet();
		if (points != null) /// add way to add created lists to pile then only spawn some depening on amount needed
		{
			spawnedPoints.AddRange(points);
			foreach (PoissonPoint point in points)
			{
				Vector3 world = point.pos.ToVector3() - offset + this.transform.position;
				int angleamount = 16;
				float y = Random.Range(0, angleamount + 1) * (360 / angleamount);
				Quaternion rotation = Quaternion.Euler(0, y, 0);
				GameObject gg = Instantiate(buildings[0], world, rotation, this.transform);
				//float h = (settlementRadius / 2 - Vector3.Distance(world, this.transform.position)) * 0.3f;
				gg.transform.localScale = Vector3.one * (point.radius / 2) + new Vector3(0, point.radius, 0);
			}
		}
	}

	private List<PoissonPoint> CreatePointSet()
	{
		List<PoissonPoint> points = new List<PoissonPoint>();
		PoissonPoint start = GetNextStartingPoint();
		for (int i = 0; i < rejectionSamples; i++)
		{
			float angle = Random.value * Mathf.PI * 2;
			Vector2 direction = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
			Vector2 candidate = start.pos + direction * Random.Range(start.radius, 2 * start.radius);
			//check against cell info

			if (!(candidate.x >= 0 && candidate.x < regionSize.x && candidate.y >= 0 && candidate.y < regionSize.y))
			{
				continue;
			}
			float sampledRadius = SampleRadiusAtPostion(candidate);
			if (PoissonSampling.DoesPointOverlapExistingPoint(poissonCells, candidate, minBuildingRadius, sampledRadius))
			{
				continue;
			}
			//sample radius at point in density map
			//float sampledRadius = ampleRadiusAtPostion(candidate));
			PoissonPoint candiatePoint = new PoissonPoint(candidate, sampledRadius);
			PoissonSampling.PushPointIntoGrid(poissonCells, candiatePoint, minBuildingRadius);
			//spawnedPoints.Add(candiatePoint);
			points.Add(candiatePoint);
			//break;

		}
		start.usedForStart = true;
		return points;
	}

	private float SampleRadiusAtPostion(Vector2 point) //localPos 0,0, being min
	{
		Vector3 worldPos = point.ToVector3() - offset;
		float heightMod = 1f - Vector3.Distance(worldPos, this.transform.position) / (regionExtent / 2);
		float randomRadius = radiusCurve.Evaluate(heightMod);
		return randomRadius;
	}

	private PoissonPoint GetNextStartingPoint()
	{
		PoissonPoint startPoint;
		if (unusedStarts.Count > 0)
		{
			//startPoint = new PoissonPoint(new Vector2( regionSize.x * Random.value , regionSize.y * Random.value), minBuildingRadius);
			startPoint = unusedStarts[Random.Range(0, unusedStarts.Count)];
			//startPoint = unusedStarts[unusedStarts.Count - 1];
		}
		else
		{
			float sampledRadius = SampleRadiusAtPostion(regionSize / 2);
			startPoint = new PoissonPoint(regionSize / 2, sampledRadius);
		}
		return startPoint;

	}

	private void OnValidate()
	{
		//Clear();
		//GenerateGrid();
	}

	private void Clear()
	{
		//todo

	}

	private void GenerateGrid()
	{
		regionSize = new Vector2(regionExtent, regionExtent);
		offset = new Vector3(regionSize.x, 0, regionSize.y) / 2.0f;
		poissonCells = PoissonSampling.CreateGrid(regionSize, minBuildingRadius);
		spawnedPoints = new List<PoissonPoint>();
	}


	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawCube(this.transform.position, new Vector3(regionExtent, 0, regionExtent));

		if (poissonCells != null)
		{
			for (int i = 0; i < poissonCells.GetLength(0); i++)
			{
				for (int j = 0; j < poissonCells.GetLength(1); j++)
				{
					if (poissonCells[i, j].pointInCell != null)
					{
						Gizmos.color = Color.red;
						Gizmos.DrawCube(new Vector3(poissonCells[i, j].x, poissonCells[i, j].pushedPoints.Count, poissonCells[i, j].z), Vector3.one * 0.4f);
						//Gizmos.DrawSphere(new Vector3(poissonCells[i, j].pointInCell.pos.x, 0, poissonCells[i, j].pointInCell.pos.y), 0.1f);
						continue;
					}

					Gizmos.color = Color.yellow;
					if (poissonCells[i, j].pushedPoints.Count > 0)
					{
						Gizmos.color = Color.green;
					}
					Gizmos.DrawCube(new Vector3(poissonCells[i, j].x, poissonCells[i, j].pushedPoints.Count, poissonCells[i, j].z), Vector3.one * 1f);
				}
			}
		}

		//Gizmos.color = Color.yellow;
		//Gizmos.DrawCube(this.transform.position, new Vector3(settlementRadius, 0, settlementRadius));


		if (spawnedPoints == null)
		{
			return;
		}
		foreach (PoissonPoint localPoint in spawnedPoints)
		{
			Gizmos.color = Color.red;
			//	Gizmos.DrawSphere(new Vector3(localPoint.pos.x, 0, localPoint.pos.y), localPoint.radius);
			Gizmos.color = Color.blue;
			Gizmos.DrawSphere(new Vector3(localPoint.pos.x, 0, localPoint.pos.y), localPoint.radius / 2);
		}
	}

}
