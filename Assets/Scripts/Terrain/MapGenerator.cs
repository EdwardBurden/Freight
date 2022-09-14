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
	public Transform waterTransfrom;
	public Transform groundTransfrom;
	public int waterHeight;

	private void Start()
	{
		Generate(settings[0]);//test
	}

	public void Generate(MapGenerationSettings mapGenerationSettings)
	{
		MapGenerationData mapGenerationData = CreateGenerationData(mapGenerationSettings);




		InstatiateMapSurface(mapGenerationSettings);
	}

	private MapGenerationData CreateGenerationData(MapGenerationSettings mapGenerationSettings)
	{
		//Create data with:
		//Island spawn points
		//Block spawn points
		//voronoi info
		return null;
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
			}
		}

	}



	//To Implement
	//Set water and ground size based on settings
	//spawn number of
}
