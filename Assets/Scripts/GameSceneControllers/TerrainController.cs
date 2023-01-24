using AStar;
using System;
using System.Collections;
using System.Collections.Generic;
using Terrain;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class TerrainController : Singleton<TerrainController>
{
	public event Action<MapGenerationData> TerrainGeneratedEvent;
	public event Action<IslandBlock> BlockAddedEvent;
	[SerializeField] private List<MapGenerationSettings> settings;

	private MapGenerator mapGenerator;
	private MapGenerationData generationData;

	//list of cells to make

	private void Awake()
	{
		_instance = this;
		mapGenerator = GetComponent<MapGenerator>();
	}


	public void GenerateMap()
	{
		//Voronoi diagram
		generationData = mapGenerator.GenerateMapData(settings[0]);
		StartCoroutine(PlaceMap());
	}

	public void AddStartingBlocks()
	{


	}

	public void AddNextBlock()
	{
		//get next cell,
		//create and spawn
		//remove cell form list
	}

	private IEnumerator PlaceMap()
	{
		mapGenerator.PlaceMapSurface(settings[0]);
		yield return new WaitForEndOfFrame();




		TerrainGeneratedEvent?.Invoke(generationData);
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
			Gizmos.DrawCube(new Vector3(generationData.islands[i].islandPoint.x, 5, generationData.islands[i].islandPoint.y), Vector3.one*2);
			Gizmos.color = Color.green;
			foreach (var item in generationData.islands[i].allBlockPoints)
			{
				Gizmos.DrawCube(new Vector3(item.x, 1+ i, item.y), Vector3.one);
			}
			Gizmos.color = Color.blue;
			foreach (var item in generationData.islands[i].cells)
			{
				Gizmos.DrawCube(new Vector3(item.sitePos.x, 2+ i, item.sitePos.z), Vector3.one);
			}
		}
	}

}
