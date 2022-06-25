using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
	public Island Islandprefab;

	private int MAXRETRY = 5;

	public int minislandDistance = 10;

	//public int islandAmount;
	public int rows;
	public int columns;
	//public int sizeX;
	//public int sizeZ;

	private List<Vector3> islandPoints = new List<Vector3>();

	void Start()
	{

		islandPoints.Clear();


		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < columns; j++)
			{
				Vector3 position = new Vector3(minislandDistance * i, 0, minislandDistance * j);
				Island island = Instantiate(Islandprefab);
				island.transform.parent = this.transform;
				island.transform.position = position;
				islandPoints.Add(position);
			}
		}


	}
}

