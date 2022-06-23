using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour
{
	public Material material;

	public int pointsAmount = 50;
	public int density = 1;
	public int maxSize = 60;
	public int minSize = 10;

	//List<List<Triangle>> triangles = new List<List<Triangle>>(); //temp
	List<VoronoiCell> voronoiCells = new List<VoronoiCell>(); //temp

	private void Start()
	{
		Random.InitState(12);
		List<Vertex> points = IslandGenerator.CreateRandomPoints(pointsAmount, minSize, maxSize);
		List<Vertex> boundary = JarvisMarchAlgorithm.GetConvexHull(points);
		//points.RemoveAll(boundary);  remove boundary points optionally
		voronoiCells = IslandGenerator.CreateVoronoiCells(points, maxSize);

		IslandGenerator.ClipVoronoiDiagram(voronoiCells, boundary);

		voronoiCells = IslandGenerator.CreateTrianglesFromVoronoiCells(voronoiCells); //may not be needed
		DrawMesh(voronoiCells[0]);

		foreach (VoronoiCell  voronoiCell in voronoiCells)
		{
			DrawMesh(voronoiCell);
		}
		//var triangles = DelaunayTriangulation.TriangulateByFlippingEdges(points);
		//create our points
		//create island boundary
		//create cells
		//make mesh from cells
	}

	private void OnDrawGizmos()
	{
		//
		for (int i = 0; i < voronoiCells.Count; i++)
		{
			for (int j = 0; j < voronoiCells[i].edges.Count; j++)
			{
				Gizmos.DrawLine(voronoiCells[i].edges[j].v1, voronoiCells[i].edges[j].v2);
			}
		}


		//
		Gizmos.color = Color.blue;

		for (int i = 0; i < voronoiCells[0].triangles.Count; i++)
		{
			Gizmos.DrawLine(voronoiCells[0].triangles[i].v1.position, voronoiCells[0].triangles[i].v2.position);
			Gizmos.DrawLine(voronoiCells[0].triangles[i].v2.position, voronoiCells[0].triangles[i].v3.position);
			Gizmos.DrawLine(voronoiCells[0].triangles[i].v3.position, voronoiCells[0].triangles[i].v1.position);
		}

		//
		Gizmos.color = Color.green;
		int nextCellPoint = 1;

		foreach (var item in voronoiCells)
		{
			for (int i = 0; i < item.borderCoordinates.Count; i++, nextCellPoint++)
			{
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

		List<Vector3> vertices = new List<Vector3>();
		List<int> triangles = new List<int>();

		float height = Random.Range(-3.0f, 3.0f);

		int p = 0;
		for (int i = 0; i < voronoiCell.triangles.Count; i++, p += 3)
		{
			Triangle triangle = voronoiCell.triangles[i];
			vertices.Add(triangle.v1.position + new Vector3(0, height, 0));
			vertices.Add(triangle.v2.position + new Vector3(0, height, 0));
			vertices.Add(triangle.v3.position + new Vector3(0, height, 0));

			triangles.Add(p);
			triangles.Add(p + 1);
			triangles.Add(p + 2);
		}
		/*	Vector3 p1 = voronoiCell.sitePos;
			vertices.Add(p1);

			for (int j = 0; j < voronoiCell.borderCoordinates.Count; j++)
			{
				Vector3 p3 = voronoiCell.borderCoordinates[j];
				int nextPoint = j + 1;
				if (nextPoint >= voronoiCell.borderCoordinates.Count)
				{
					nextPoint = 0;
				}
				Vector3 p2 = voronoiCell.borderCoordinates[nextPoint];

				vertices.Add(p2);
				vertices.Add(p3);

				triangles.Add(0);
				triangles.Add(vertices.Count - 2);
				triangles.Add(vertices.Count - 1);
			}

			*/

		Mesh triangleMesh = new Mesh();
		triangleMesh.vertices = vertices.ToArray();
		triangleMesh.triangles = triangles.ToArray();
		triangleMesh.RecalculateNormals();
		MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
		MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
		meshRenderer.material = material;
		gameObject.transform.parent = this.transform;
			//gameObject.transform.position = voronoiCell.sitePos;
		meshFilter.mesh = triangleMesh;
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
