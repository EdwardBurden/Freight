using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IslandGenerator
{
	public static Mesh CreateVoronoiMesh(VoronoiCell voronoiCell, int height)
	{
		List<Vector3> vertices = new List<Vector3>();
		List<int> triangles = new List<int>();

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

		for (int j = 0; j < voronoiCell.borderCoordinates.Count; j++)
		{
			Vector3 p3 = voronoiCell.borderCoordinates[j] + new Vector3(0, height, 0);
			int nextPoint = j + 1;
			if (nextPoint >= voronoiCell.borderCoordinates.Count)
			{
				nextPoint = 0;
			}
			Vector3 p2 = voronoiCell.borderCoordinates[nextPoint] + new Vector3(0, height, 0);


			vertices.Add(p2 + new Vector3(0, -height, 0));
			vertices.Add(p3);
			vertices.Add(p2);



			triangles.Add(vertices.Count - 3);
			triangles.Add(vertices.Count - 2);
			triangles.Add(vertices.Count - 1);


			vertices.Add(p2 + new Vector3(0, -height, 0));
			vertices.Add(p3 + new Vector3(0, -height, 0));
			vertices.Add(p3);

			triangles.Add(vertices.Count - 3);
			triangles.Add(vertices.Count - 2);
			triangles.Add(vertices.Count - 1);
		}


		Mesh triangleMesh = new Mesh();
		triangleMesh.vertices = vertices.ToArray();
		triangleMesh.triangles = triangles.ToArray();
		triangleMesh.RecalculateNormals();
		return triangleMesh;
	}


	public static List<VoronoiCell> CreateTrianglesFromVoronoiCells(List<VoronoiCell> voronoiCells)
	{
		List<VoronoiCell> meshCells = new List<VoronoiCell>();
		foreach (VoronoiCell voronoiCell in voronoiCells)
		{
			if (voronoiCell.borderCoordinates.Count ==0)
			{
				continue;
			}
			voronoiCell.triangles = DelaunayTriangulation.TriangulateByFlippingEdges(voronoiCell.borderCoordinates);
			meshCells.Add(voronoiCell);
		}
		return meshCells;
	}


	public static List<Vertex> CreateRandomPoints(int pointsAmount, int minSize, int maxSize)
	{
		List<Vertex> points = new List<Vertex>();
		for (int i = 0; i < pointsAmount; i++)
		{
			int distance = Random.Range(minSize, maxSize);
			float xChance = Random.Range(-1.0f, 1.0f);
			float zChance = Random.Range(-1.0f, 1.0f);
			Vector3 randomDirection = new Vector3(xChance, 0, zChance);
			Vector3 positon = randomDirection.normalized * distance;
			points.Add(new Vertex(positon));
		}
		return points;
	}

	public static List<VoronoiCell> CreateVoronoiCells(List<Vertex> vectors, int maxSize)
	{
		List<Vector3> points = GetPostionsFromVertecies(vectors);

		float bigNumber = maxSize * 5;
		points.Add(new Vector3(0f, 0f, bigNumber));
		points.Add(new Vector3(0f, 0f, -bigNumber));
		points.Add(new Vector3(bigNumber, 0f, 0f));
		points.Add(new Vector3(-bigNumber, 0f, 0f));
		var cells = DelaunayToVoronoi.GenerateVoronoiDiagram(points);
		if (cells.Count != points.Count) {
			Debug.LogError("error");
		}
		return cells;
	}

	public static void ClipVoronoiDiagram(List<VoronoiCell> voronoiCells, List<Vertex> boundary)
	{
		List<Vector3> points = GetPostionsFromVertecies(boundary);
		foreach (VoronoiCell voronoiCell in voronoiCells)
		{
			voronoiCell.borderCoordinates = GetCellEdgePositions(voronoiCell);
			voronoiCell.borderCoordinates = SutherlandHodgman.ClipPolygon(voronoiCell.borderCoordinates, points);
		}

	}

	private static List<Vector3> GetPostionsFromVertecies(List<Vertex> vertices)
	{
		List<Vector3> points = new List<Vector3>();
		foreach (Vertex vertex in vertices)
		{
			points.Add(vertex.position);
		}
		return points;
	}

	private static List<Vector3> GetCellEdgePositions(VoronoiCell voronoiCell)
	{


			List<Vector3> edgesCoordinates = new List<Vector3>();

		VoronoiEdge startEdge = voronoiCell.edges[0];

		edgesCoordinates.Add(startEdge.v2);

		Vector3 currentVertex = startEdge.v2;

		for (int j = 1; j < voronoiCell.edges.Count; j++)
		{
			//Find the next edge
			for (int k = 1; k < voronoiCell.edges.Count; k++)
			{
				Vector3 thisEdgeStart = voronoiCell.edges[k].v1;

				if ((thisEdgeStart - currentVertex).sqrMagnitude < 0.0001f)
				{
					edgesCoordinates.Add(voronoiCell.edges[k].v2);

					currentVertex = voronoiCell.edges[k].v2;

					break;
				}
			}
		}
		return edgesCoordinates;
	}

	/*private void OnDrawGizmos()
	{
		float radius = 1f;
		Gizmos.color = Color.green;
		List<Vector3> vectors = CreateVerterListFromPoints();
		for (int i = 0; i < vectors.Count; i++)
		{


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

		Gizmos.color = Color.magenta;
		for (int i = 0; i < cells.Count; i++)
		{


			Gizmos.DrawSphere(cells[i].sitePos, radius);
		}


		Gizmos.color = Color.red;
		DisplayVoronoiCells(cells);
		for (int i = 0; i < cells.Count; i++)
		{
			for (int j = 0; j < cells[i].edges.Count; j++)
			{
				Gizmos.DrawLine(cells[i].edges[j].v1, cells[i].edges[j].v2);
			}
		}
		Gizmos.color = Color.green;
		int nextCellPoint = 1;

		foreach (var item in cells)
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



	}*/
}
