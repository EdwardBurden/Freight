using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FloorMaker
{
	private const int DENSITY = 1;


	public static Mesh CreateQuad(int length, int width)
	{
		Mesh mesh = new Mesh();
		mesh.vertices = CreateQuadVertices(length, width);
		mesh.triangles = CreateQuadTriangles(length, width);
		//mesh.colors = CreateQuadColors(length,width);
		mesh.RecalculateNormals();
		return mesh;
	}
	private static Color[] CreateQuadColors(int length, int width)
	{
		Color[] colors = new Color[(length + 1) * (width + 1)];
		return colors;
	}

	private static Vector3[] CreateQuadVertices(int length, int width)
	{
		Vector3[] vertices = new Vector3[(length + 1) * (width + 1)];
		int i = 0;
		for (int z = 0; z <= width; z++)
		{
			for (int x = 0; x <= length; x++)
			{
				float y = Mathf.PerlinNoise(x*0.1f, z*0.1f);
				vertices[i] = new Vector3(x, y, z);
				i++;
			}
		}
		return vertices;
	}

	private static int[] CreateQuadTriangles(int length, int width)
	{
		int[] triangles = new int[length * width * 6];
		int verts = 0;
		int tris = 0;
		for (int z = 0; z < width; z++)
		{
			for (int x = 0; x < length; x++)
			{
				triangles[tris + 0] = verts + 0;
				triangles[tris + 1] = verts + length + 1;
				triangles[tris + 2] = verts + 1;
				triangles[tris + 3] = verts + 1;
				triangles[tris + 4] = verts + length + 1;
				triangles[tris + 5] = verts + length + 2;
				verts++;
				tris += 6;
			}
			verts++;
		}
		return triangles;
	}
}
