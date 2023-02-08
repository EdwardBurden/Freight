using AStar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using Terrain;
using Unity.Mathematics;
using Unity.VisualScripting;
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


		//have curve 0-1 whith spawn chance on y axis
		//then scale 0-1 to length of possible cells 
		//while loop through all cells and get ranom number form curve , with max attempts const
		float distance = 1000000;
		IslandGenerationData island = null;
		VoronoiCell cell = null;
		for (int i = 0; i < islands.Count; i++) //TODO CLEAN UP
		{
			foreach (var item in islands[i].GetUnplacedCells())
			{
				float d = Vector3.Distance(item.sitePos, Vector3.zero);
				if (d < distance)
				{
					distance = d;
					island = islands[i];
					cell = item;
				}
			}
		}
		IslandBlock block = mapGenerator.CreateIslandBlock(island.islandBase.transform, cell, settings[0]);
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
			Gizmos.color = Color.green;
			for (int b = 0; b < generationData.islands[i].boundary.Count; b++)
			{
				int bE = b + 1;
				if (bE == generationData.islands[i].boundary.Count)
				{
					bE = 0;
				}
				Gizmos.DrawLine(generationData.islands[i].boundary[b].position, generationData.islands[i].boundary[bE].position);
			}
			foreach (var item in generationData.islands[i].boundary)
			{
				Gizmos.DrawCube(new Vector3(item.position.x, 2 + i, item.position.z), Vector3.one);
			}
		}
	}

	public bool CanPlaceBlock()
	{
		return !generationData.AllBlocksPlaced();
	}
}
