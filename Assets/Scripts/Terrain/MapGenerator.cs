using System;
using System.Collections;
using System.Collections.Generic;
using Terrain;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
	private const int TILESIZE = 10;

	[Header("Map Settings")]
	[SerializeField]
	private List<MapGenerationSettings> settings;

	[Header("Map Surface")]
	[SerializeField]
	private GameObject waterPrefab;
	[SerializeField]
	private GameObject groundPrefab;
	[SerializeField]
	private GameObject borderprefab;
	[SerializeField]
	private Transform waterTransfrom;
	[SerializeField]
	private Transform groundTransfrom;
	[SerializeField]
	private Transform borderTransform;
	[SerializeField]
	private Transform islandsTransform;
	[SerializeField]
	private int waterHeight;

	[Header("island Generation")]
	[SerializeField]
	private Material[] islandMaterials;

	private MapGenerationData generationData;

	private void Start()
	{
		Generate(settings[0]);
	}

	private Vector3 GetMapCenterOffset(MapGenerationSettings mapGenerationSettings)
	{
		return MapGeneration.GetAreaOffset(mapGenerationSettings.mapSize) * TILESIZE;
	}

	public void Generate(MapGenerationSettings settings)
	{
		generationData = CreateGenerationData(settings);
		InstatiateMapSurface(settings);
		InstatiateIslands(settings, generationData);
	}

	private MapGenerationData CreateGenerationData(MapGenerationSettings settings)
	{
		MapGenerationData mapGenerationData = new MapGenerationData();
		List<Vector2> islandpoints = PoissonSampling.CreateRandomPoints(settings.mapSize * TILESIZE, settings.islandRadius);
		foreach (Vector2 islandpoint in islandpoints)
		{
			IslandGenerationData islandData = CreateIslandData(islandpoint, settings.islandSize, settings.islandPointRadius);
			mapGenerationData.islands.Add(islandData);
		}
		return mapGenerationData;
	}

	private IslandGenerationData CreateIslandData(Vector2 islandPoint, Vector2 area, float islandPointRadius)
	{
		List<Vector2> islandpoints = PoissonSampling.CreateRandomPoints(area, islandPointRadius);
		List<Vertex> boundarypoints = islandpoints.ToVertices();
		List<Vertex> verices = islandpoints.ToVertices();
		var boundary = JarvisMarchAlgorithm.GetConvexHull(boundarypoints);
		var cells = MapGeneration.CreateVoronoiCells(verices);
		MapGeneration.ClipVoronoiDiagram(cells, boundary);
		cells = MapGeneration.CreateTrianglesFromVoronoiCells(cells);

		IslandGenerationData data = new IslandGenerationData();
		data.center = islandPoint;
		data.boundary = boundary;
		data.voronoiCells = cells;
		data.allRandomPoints = islandpoints;
		return data;
	}

	private void InstatiateMapSurface(MapGenerationSettings settings)
	{
		GameObjectUtils.Clear(waterTransfrom);
		GameObjectUtils.Clear(groundTransfrom);
		GameObjectUtils.Clear(borderTransform);
		Vector3 mapOffset = GetMapCenterOffset(settings);
		float tileRadius = TILESIZE / 2.0f;
		Vector3 offset = mapOffset - new Vector3(tileRadius, 0, tileRadius);
		for (int i = 0; i < settings.mapSize.x; i++)
		{
			for (int j = 0; j < settings.mapSize.y; j++)
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
				if (i == settings.mapSize.x - 1)
				{
					Instantiate(borderprefab, water, Quaternion.Euler(0, 90, 0), borderTransform);
				}
				if (j == settings.mapSize.y - 1)
				{
					Instantiate(borderprefab, water, Quaternion.Euler(0, 0, 0), borderTransform);
				}
			}
		}

	}

	private void InstatiateIslands(MapGenerationSettings settings, MapGenerationData data)
	{
		Vector3 offset = GetMapCenterOffset(settings);
		foreach (IslandGenerationData island in data.islands)
		{
			int baseIndex = UnityEngine.Random.Range(0, settings.bases.Count);
			Vector3 basePosition = new Vector3(island.center.x, 0, island.center.y) - offset;
			GameObject islandbase = Instantiate(settings.bases[baseIndex], basePosition, Quaternion.Euler(0, 0, 0), islandsTransform);

			foreach (VoronoiCell voronoiCell in island.voronoiCells)
			{
				Vector3 sitePos = voronoiCell.sitePos - MapGeneration.GetAreaOffset(settings.islandSize) + basePosition;
				RayCastInfo<IslandBase> raycastHit = RayCastUtils.FindFirstObjectHit<IslandBase>(sitePos + new Vector3(0, 10, 0), Vector3.down);
				if (raycastHit.focused && raycastHit.focusObject == islandbase.GetComponentInChildren<IslandBase>())
				{
					float distance = Vector3.Distance(sitePos, basePosition);
					GameObject mesh = DrawMesh(settings, islandbase.transform, voronoiCell, basePosition, distance);
					island.cells.Add(mesh);
				}
			}
		}
	}

	private GameObject DrawMesh(MapGenerationSettings settings, Transform parent, VoronoiCell voronoiCell, Vector3 center, float distance)
	{
		GameObject gameObject = new GameObject("Cell");
		gameObject.transform.parent = parent;
		gameObject.transform.position = center - MapGeneration.GetAreaOffset(settings.islandSize);

		int materialIndex = UnityEngine.Random.Range(0, islandMaterials.Length);
		float distanceHeight = Mathf.Min(5, (settings.islandRadius / distance) * 0.2f);
		float perlinHeight = Mathf.PerlinNoise(voronoiCell.sitePos.x * 300, voronoiCell.sitePos.y * 300) * 10;
		int height = Mathf.CeilToInt(distanceHeight + perlinHeight);

		MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
		MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
		meshRenderer.material = islandMaterials[materialIndex];
		meshFilter.mesh = MapGeneration.CreateVoronoiMesh(voronoiCell, height);
		return gameObject;
	}
}
