using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour
{
	public Material[] materials;

	public int minPoints = 50;
	public int maxPoints = 50;
	public int minRadius = 10;
	public int maxRadius = 60;

	public int seed;

	public int pointsAmount;

	List<Vertex> points = new List<Vertex>();
	List<Vertex> boundary = new List<Vertex>();
	//List<List<Triangle>> triangles = new List<List<Triangle>>(); //temp
	List<VoronoiCell> voronoiCells = new List<VoronoiCell>(); //temp

	private void Start()
	{
		//Random.InitState(019283);
		//seed = Random.seed;
		print(Random.state.ToString());
		pointsAmount = Random.Range(minPoints, maxPoints);

		//Random.InitState(12);
		points = IslandGenerator.CreateRandomPoints(pointsAmount, minRadius, maxRadius);
		var _myLocalList = new List<Vertex>(points);
		boundary = JarvisMarchAlgorithm.GetConvexHull(_myLocalList);

		voronoiCells = IslandGenerator.CreateVoronoiCells(points, maxRadius);

		IslandGenerator.ClipVoronoiDiagram(voronoiCells, boundary);

		voronoiCells = IslandGenerator.CreateTrianglesFromVoronoiCells(voronoiCells);


		foreach (VoronoiCell voronoiCell in voronoiCells)
		{

			DrawMesh(voronoiCell);
		}
	}

	private void OnDrawGizmos()
	{
		//
		/*	Gizmos.color = Color.red;
			for (int i = 0; i < voronoiCells.Count; i++)
			{
				for (int j = 0; j < voronoiCells[i].edges.Count; j++)
				{
					Gizmos.DrawLine(voronoiCells[i].edges[j].v1, voronoiCells[i].edges[j].v2);
				}
			}

			*/
		//
		Gizmos.color = Color.blue;
		int nextvert = 1;
		for (int i = 0; i < boundary.Count; i++, nextvert++)
		{
			if (nextvert == boundary.Count)
			{
				nextvert = 0;
			}
			Gizmos.DrawLine(boundary[i].position, boundary[nextvert].position);
		}

		//
		Gizmos.color = Color.gray;


		foreach (var item in points)
		{
			//Gizmos.DrawSphere(item.position, 2);
		}
		Gizmos.color = Color.green;
		foreach (var item in voronoiCells)
		{
			int nextCellPoint = 1;
			//Gizmos.DrawSphere(item.sitePos, 1);


			for (int i = 0; i < item.borderCoordinates.Count; i++, nextCellPoint++)
			{
				//Gizmos.DrawSphere(item.borderCoordinates[i], 0.5f);
				if (nextCellPoint == item.borderCoordinates.Count)
				{
					nextCellPoint = 0;
				}
				Gizmos.DrawLine(item.borderCoordinates[i], item.borderCoordinates[nextCellPoint]);
			}
		}
	}

	private void DrawMesh(VoronoiCell voronoiCell)
	{
		GameObject gameObject = new GameObject("Cell");

		int h = Random.Range(0, materials.Length);

		//	float dist = Vector2.Distance(voronoiCell.sitePos.xz(), Vector2.zero);
		//	int height = Random.Range(1, 5);

		Mesh mesh = IslandGenerator.CreateVoronoiMesh(voronoiCell, (h + 1) * 5);

		MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
		MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
		meshRenderer.material = materials[h];
		gameObject.transform.parent = this.transform;
		gameObject.transform.localPosition = Vector3.zero;
		meshFilter.mesh = mesh;
	}


	private void DisplayVoronoiCells(List<VoronoiCell> cells)
	{
		Random.InitState(6);

		for (int i = 0; i < cells.Count; i++)
		{
			VoronoiCell c = cells[i];

			Vector3 p1 = c.sitePos;

			Gizmos.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);

			List<Vector3> vertices = new List<Vector3>();

			List<int> triangles = new List<int>();

			vertices.Add(p1);

			for (int j = 0; j < c.edges.Count; j++)
			{
				Vector3 p3 = c.edges[j].v1;
				Vector3 p2 = c.edges[j].v2;

				vertices.Add(p2);
				vertices.Add(p3);

				triangles.Add(0);
				triangles.Add(vertices.Count - 2);
				triangles.Add(vertices.Count - 1);
			}

			Mesh triangleMesh = new Mesh();

			triangleMesh.vertices = vertices.ToArray();

			triangleMesh.triangles = triangles.ToArray();

			triangleMesh.RecalculateNormals();

			Gizmos.DrawMesh(triangleMesh);
		}
	}
}
