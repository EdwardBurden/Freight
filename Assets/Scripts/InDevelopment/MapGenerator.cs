using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
	public Island Islandprefab;

	private int MAXRETRY = 5;

	public int minislandDistance = 10;

	public int islandAmount;
	public int sizeX;
	public int sizeZ;

	private List<Vector3> islandPoints = new List<Vector3>();

	void Start()
	{

		islandPoints.Clear();

		for (int i = 0; i < islandAmount; i++)
		{
			Vector3 position = Vector3.zero;
			for (int j = 0; j < MAXRETRY; j++)
			{
				int x = Random.Range(0, sizeX);
				int z = Random.Range(0, sizeZ);
				Vector3 testPos = new Vector3(x, 0, z);
				if (CanAddIsland(testPos))
				{
					position = testPos;
					break;
				}
			}

			Island island =  Instantiate(Islandprefab);
			island.transform.parent = this.transform;
			island.transform.position = position;
			islandPoints.Add(position);
		}
	}

	private bool CanAddIsland(Vector3 position)
	{
		for (int i = 0; i < islandPoints.Count; i++)
		{
			if (Vector3.Distance(position, islandPoints[i]) < minislandDistance)
			{
				return false;
			}
		}
		return true;

	}


}

