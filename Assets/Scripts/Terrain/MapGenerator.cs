using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
	public GameObject floor;
	public Material floorMat;

	public void Generate(int sizeX, int sizeZ)
	{
		MeshFilter meshFilter = floor.GetComponent<MeshFilter>();
		meshFilter.mesh = FloorMaker.CreateQuad(sizeX, sizeZ);
		MeshRenderer meshRenderer = floor.GetComponent<MeshRenderer>();
		meshRenderer.material = floorMat;
	}
}

