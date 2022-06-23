using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IslandGenerator
{



	public static List<VoronoiCell> CreateTrianglesFromVoronoiCells(List<VoronoiCell> voronoiCells)
	{
		List<VoronoiCell> meshCells = new List<VoronoiCell>();
		foreach (VoronoiCell voronoiCell in voronoiCells)
		{
			if (voronoiCell.borderCoordinates.Count < 3)
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
			int size = Random.Range(minSize, maxSize);
			float xChance = Random.Range(-1.0f, 1.0f);
			float zChance = Random.Range(-1.0f, 1.0f);
			Vector3 randomDirection = new Vector3(xChance, 0, zChance);
			Vector3 positon = randomDirection.normalized * size;
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

				if ((thisEdgeStart - currentVertex).sqrMagnitude < 0.01f)
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
