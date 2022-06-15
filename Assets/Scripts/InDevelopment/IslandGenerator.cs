using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandGenerator : MonoBehaviour
{
	//include this positon in the algorithm

	//  https://www.habrador.com/tutorials/math/8-convex-hull/
	public int boundaryPointsAmount = 20;
	public int maxSize = 30;
	public int minSize = 10;

	private Transform[] points;
	private List<Vertex> boundary = new List<Vertex>();
	private List<Triangle> triangles = new List<Triangle>();
	private List<VoronoiCell> cells = new List<VoronoiCell>();

	private void Start()
	{
		points = new Transform[boundaryPointsAmount];
		SpawnPoints();
	//}

	//void Update()
	//{
		//points = new Vector3[boundaryPointsAmount + 1];
		//SpawnPoints();
		List<Vertex> vertices = CreateVertexListFromPoints();
		boundary = JarvisMarchAlgorithm.GetConvexHull(vertices);
		triangles = DelaunayTriangulation.TriangulateByFlippingEdges(CreateVerterListFromPoints());

		float bigSize = size * 2f;
		List<Vector3> vectors = CreateVerterListFromPoints();
		vectors.Add(new Vector3(0f, 0f, bigSize) + this.transform.position);
		vectors.Add(new Vector3(0f, 0f, -bigSize) + this.transform.position);
		vectors.Add(new Vector3(bigSize, 0f, 0f) + this.transform.position);
		vectors.Add(new Vector3(-bigSize, 0f, 0f) + this.transform.position);
		vectors.RemoveAll(x => boundary.Exists(v => v.position == x));

		cells = DelaunayToVoronoi.GenerateVoronoiDiagram(vectors);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		List<Vector3> vectors = CreateVerterListFromPoints();
		for (int i = 0; i < vectors.Count; i++)
		{
			float radius = 0.2f;

			Gizmos.DrawSphere(vectors[i], radius);
		}


		Gizmos.color = Color.white;
		int nextPoint = 1;
		for (int i = 0; i < boundary.Count; i++, nextPoint++)
		{
			if (nextPoint == boundary.Count)
			{
				nextPoint = 0;
			}
			Gizmos.DrawLine(boundary[i].position, boundary[nextPoint].position);
		}
		Gizmos.color = Color.blue;
		for (int i = 0; i < triangles.Count; i++)
		{
			Gizmos.DrawLine(triangles[i].v1.position, triangles[i].v2.position);
			Gizmos.DrawLine(triangles[i].v2.position, triangles[i].v3.position);
			Gizmos.DrawLine(triangles[i].v3.position, triangles[i].v1.position);
		}
		Gizmos.color = Color.red;
		//DisplayVoronoiCells(cells);
		for (int i = 0; i < cells.Count; i++)
		{
			for (int j = 0; j < cells[i].edges.Count; j++)
			{
				Gizmos.DrawLine(cells[i].edges[j].v1, cells[i].edges[j].v2);
			}
		}

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


	private List<Vertex> CreateVertexListFromPoints()
	{
		List<Vertex> vertices = new List<Vertex>();
		foreach (var item in points)
		{
			vertices.Add(new Vertex(item.position));
		}
		return vertices;
	}

	private List<Vector3> CreateVerterListFromPoints()
	{
		List<Vector3> vertices = new List<Vector3>();
		foreach (var item in points)
		{
			vertices.Add(item.position);
		}
		return vertices;
	}



	public void SpawnPoints()
	{
		int size = Random.Range(minSize, maxSize);
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
