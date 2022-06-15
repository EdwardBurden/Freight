using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandGenerator : MonoBehaviour
{
	//include this positon in the algorithm

	//  https://www.habrador.com/tutorials/math/8-convex-hull/
	public int boundaryPointsAmount = 20;
	public int size = 10;

	private Transform[] points;
	private List<Vertex> boundary = new List<Vertex>();

	private void Start()
	{
		points = new Transform[boundaryPointsAmount ];
		SpawnPoints();
	}

	void Update()
	{
		//points = new Vector3[boundaryPointsAmount + 1];
		//SpawnPoints();
		List<Vertex> vertices = CreateVertexListFromPoints();
		boundary = JarvisMarchAlgorithm.GetConvexHull(vertices);
	}

	private void OnDrawGizmos()
	{
		int nextPoint = 1;
		for (int i = 0; i < boundary.Count; i++, nextPoint++)
		{
			if (nextPoint == boundary.Count)
			{
				nextPoint = 0;
			}

			Gizmos.DrawLine(this.transform.position + boundary[i].position, this.transform.position + boundary[nextPoint].position);
		}

	}

	private List<Vertex> CreateVertexListFromPoints()
	{
		List<Vertex> vertices = new List<Vertex>();
		foreach (var item in points)
		{
			vertices.Add(new Vertex(item.localPosition));
		}
		return vertices;
	}



	public void SpawnPoints()
	{
		for (int i = 0; i < boundaryPointsAmount; i++)
		{
			float xChance = Random.Range(-1.0f, 1.0f);
			float zChance = Random.Range(-1.0f, 1.0f);
			float sizeChance = Random.Range(0.0f, 1.0f);
			Vector3 randomDirection = new Vector3(xChance, 0, zChance);
			Vector3 positon = randomDirection.normalized * size * sizeChance;

			GameObject gameObject = new GameObject("Point");
			gameObject.transform.parent = this.transform;
			gameObject.transform.localPosition = positon;
			points[i] = gameObject.transform;
		}

	}

}
