using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
	private const int TILESIZE = 10;

	public List<MapGenerationSettings> settings;

	[Header("Map Surface")]
	public GameObject waterPrefab;
	public GameObject groundPrefab;
	public GameObject borderprefab;
	public Transform waterTransfrom;
	public Transform groundTransfrom;
	public Transform borderTransform;
	public Transform islandsTransform;
	public int waterHeight;

	[Header("island Generation")]
	public GameObject pointPrefab; //temp




	private void Start()
	{
		Generate(settings[0]);//test
	}

	private Vector3 GetMapCenterOffset(MapGenerationSettings mapGenerationSettings)
	{
		float xOffset = (mapGenerationSettings.mapSize.x / 2.0f) * TILESIZE;
		float yOffset = (mapGenerationSettings.mapSize.y / 2.0f) * TILESIZE;
		return new Vector3(xOffset, 0, yOffset);
	}

	public void Generate(MapGenerationSettings mapGenerationSettings)
	{
		MapGenerationData mapGenerationData = CreateGenerationData(mapGenerationSettings);
		InstatiateMapSurface(mapGenerationSettings);
		InstatiateIslands(mapGenerationSettings, mapGenerationData);
	}

	private void InstatiateIslands(MapGenerationSettings mapGenerationSettings, MapGenerationData mapGenerationData)
	{
		Vector3 offset = GetMapCenterOffset(mapGenerationSettings);
		foreach (IslandGenerationData island in mapGenerationData.islandGenerationDatas)
		{
			Vector3 worldSpacepoint = new Vector3(island.center.x, 0, island.center.y) - offset;
			Instantiate(pointPrefab, worldSpacepoint, Quaternion.Euler(0, 0, 0), islandsTransform);
			foreach (Vector2 spot in island.spots)
			{
				GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
				Vector3 spot3d = new Vector3(spot.x, 4, spot.y) - (new Vector3(mapGenerationSettings.islandSize.x / 2.0f, 0, mapGenerationSettings.islandSize.y / 2.0f));
				gameObject.transform.position = spot3d + worldSpacepoint;
			}
		}
	}

	private MapGenerationData CreateGenerationData(MapGenerationSettings mapGenerationSettings)
	{
		MapGenerationData mapGenerationData = new MapGenerationData();
		List<Vector2> islandpoints = PoissonSampling.CreateRandomPoints(mapGenerationSettings.mapSize * TILESIZE, mapGenerationSettings.islandRadius);
		foreach (Vector2 islandpoint in islandpoints)
		{
			IslandGenerationData islandData = CreateIslandData(islandpoint, mapGenerationSettings.islandSize, mapGenerationSettings.islandSpotRadius);
			mapGenerationData.islandGenerationDatas.Add(islandData);
		}
		return mapGenerationData;
	}

	private IslandGenerationData CreateIslandData(Vector2 islandPoint, Vector2 area, float spotRadius)
	{
		IslandGenerationData islandGenerationData = new IslandGenerationData();
		islandGenerationData.center = islandPoint;
		List<Vector2> islandpoints = PoissonSampling.CreateRandomPoints(area, spotRadius); //put 1 soemthwere later

		islandGenerationData.spots = islandpoints;
		return islandGenerationData;
	}


	private void InstatiateMapSurface(MapGenerationSettings mapGenerationSettings)
	{
		float xOffset = (mapGenerationSettings.mapSize.x / 2.0f) * TILESIZE;
		float yOffset = (mapGenerationSettings.mapSize.y / 2.0f) * TILESIZE;
		float tileRadius = TILESIZE / 2.0f;
		Vector3 offset = new Vector3(xOffset - tileRadius, 0, yOffset - tileRadius);
		GameObjectUtils.Clear(waterTransfrom);
		GameObjectUtils.Clear(groundTransfrom);
		for (int i = 0; i < mapGenerationSettings.mapSize.x; i++)
		{
			for (int j = 0; j < mapGenerationSettings.mapSize.y; j++)
			{
				Vector3 pos = new Vector3(i, 0, j) * TILESIZE;
				Vector3 ground = (pos - offset);
				Vector3 water = ground + new Vector3(0, waterHeight, 0);
				Instantiate(waterPrefab, water, Quaternion.identity, waterTransfrom);
				Instantiate(groundPrefab, ground, Quaternion.identity, groundTransfrom);


				if (i == 0)
				{
					Instantiate(borderprefab, water, Quaternion.Euler(0, 270, 0), borderTransform);
				}
				if (j == 0)
				{
					Instantiate(borderprefab, water, Quaternion.Euler(0, 180, 0), borderTransform);
				}
				if (i == mapGenerationSettings.mapSize.x - 1)
				{
					Instantiate(borderprefab, water, Quaternion.Euler(0, 90, 0), borderTransform);
				}
				if (j == mapGenerationSettings.mapSize.y - 1)
				{
					Instantiate(borderprefab, water, Quaternion.Euler(0, 0, 0), borderTransform);
				}

			}
		}

	}



	//To Implement
	//Set water and ground size based on settings
	//spawn number of
}
