using AStar;
using System;
using System.Collections;
using System.Collections.Generic;
using Terrain;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class TerrainController : Singleton<TerrainController>, IGameController
{
	public event Action<MapGenerationData> TerrainGeneratedEvent;

	[SerializeField] private List<MapGenerationSettings> settings;

	private MapGenerator mapGenerator;
	private MapGenerationData generationData;

	private void Awake()
	{
		_instance = this;
		mapGenerator = GetComponent<MapGenerator>();
	}

	public void Init()
	{
		RemoveMap();
	}

	private void RemoveMap()
	{
		mapGenerator.ClearMap();
	}

	public void GenerateMap()
	{
		generationData = mapGenerator.GenerateMapData(settings[0]);
		PlaceMap();
	}

	public IslandBlock AddNextBlock()
	{
		List<IslandGenerationData> islands = generationData.IslandsWithUnplacedBlocks();
		int random = Random.Range(0, islands.Count);
		IslandGenerationData island = islands[random];
		IslandBlock block = mapGenerator.CreateIslandBlock(island.islandBase.transform, island.GetNextCell(), settings[0]);
		island.placedBlocks.Add(block);
		return block;
	}

	private void PlaceMap()
	{
		mapGenerator.PlaceMapSurface(settings[0]);
		//yield return new WaitForEndOfFrame();
		foreach (IslandGenerationData islandData in generationData.islands)
		{
			GameObject island = mapGenerator.PlaceIslandBase(settings[0], islandData.islandPoint);
			for (int i = islandData.cells.Count - 1; i >= 0; i--)
			{
				if (!mapGenerator.CanPlaceCell(islandData.cells[i], island))
				{
					islandData.cells.RemoveAt(i);
				}
			}
			islandData.islandBase = island;
		}
		TerrainGeneratedEvent?.Invoke(generationData);
	}

	public Vector3 FindAvailablePlaceOnTerrain()
	{
		return mapGenerator.FindCentralEmptyGridPostion(settings[0]);
	}


	private void OnDrawGizmos()
	{
		if (generationData == null)
		{
			return;
		}

		for (int i = 0; i < generationData.islands.Count; i++)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawCube(new Vector3(generationData.islands[i].islandPoint.x, 5, generationData.islands[i].islandPoint.y), Vector3.one * 2);
			Gizmos.color = Color.blue;
			foreach (var item in generationData.islands[i].cells)
			{
				Gizmos.DrawCube(new Vector3(item.sitePos.x, 2 + i, item.sitePos.z), Vector3.one);
			}
		}
	}

	public bool CanPlaceBlock()
	{
		return !generationData.AllBlocksPlaced();
	}
}
