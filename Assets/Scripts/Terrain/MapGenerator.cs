using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
	public List<GameObject> islandBases;
	public List<GameObject> islandBlocks;
	public List<GameObject> buildings;

	public int islandsAmount;
	public int minBlocks;
	public int maxBlocks;
	public float islandSize;
	public float minBlockDistance;
	public float minIslandDistance;

	public Vector2 mapSize;

	private void Start()
	{
		List<Vector3> islands = new List<Vector3>();

		for (int i = 0; i < islandsAmount; i++)
		{
			if (FindValidPostion(10, minIslandDistance, mapSize, islands, out Vector3 position))
			{
				islands.Add(Createisland(position));
			}
		}
	}
	private static bool FindValidPostion(int attempts, float minDistance, Vector2 size, List<Vector3> illegalPostions, out Vector3 pos)
	{
		bool found = false;
		int counter = 0;
		while (!found && counter < attempts)
		{
			found = true;
			float x = Random.Range(-size.x, size.x);
			float z = Random.Range(-size.y, size.y);
			pos = new Vector3(x, 0, z);
			foreach (Vector3 illPos in illegalPostions)
			{
				if (Vector3.Distance(illPos, pos) < minDistance)
				{
					found = false;
				}
			}
			if (found)
			{
				return true;
			}

			counter++;
		}
		pos = Vector3.one;
		return false;
	}


	private Vector3 Createisland(Vector3 positon)
	{
		List<Vector3> blocks = new List<Vector3>();
		IslandBase islandBase = AddIsland(positon);
		int blockAmount = Random.Range(minBlocks, maxBlocks + 1);
		for (int i = 0; i < blockAmount; i++)
		{
			if (FindValidPostion(10, minBlockDistance, new Vector2(islandSize, islandSize), blocks, out Vector3 position))
			{
				AddBlock(islandBase, position);
				blocks.Add(position);
			}

		}
		return islandBase.transform.position;
	}

	private void AddBlock(IslandBase islandBase, Vector3 position)
	{
		int blockIndex = Random.Range(0, islandBlocks.Count);
		GameObject block = Instantiate(islandBlocks[blockIndex], islandBase.transform);
		float degree = Random.Range(0.0f, 360.0f);

		IslandBlock islandBlock = block.GetComponent<IslandBlock>();
		AddBuilding(islandBlock.buildingSpawnPoint.transform);


		block.transform.localPosition = position;
		block.transform.rotation = Quaternion.Euler(0, degree, 0);
		islandBase.islandBlocks.Add(islandBlock);
	}

	private void AddBuilding(Transform block)
	{
		int blockIndex = Random.Range(0, buildings.Count);
		GameObject buildingPrefab = buildings[blockIndex];
		GameObject building = Instantiate(buildingPrefab, block);
		building.transform.localPosition = Vector3.zero;
	}

	private IslandBase AddIsland(Vector3 positon)
	{
		int baseIndex = Random.Range(0, islandBases.Count);
		GameObject islandBase = Instantiate(islandBases[baseIndex]);
		float degree = Random.Range(0.0f, 360.0f);

		islandBase.transform.position = positon;
		islandBase.transform.rotation = Quaternion.Euler(0, degree, 0);
		return islandBase.GetComponent<IslandBase>();
	}
}
