using System;
using System.Collections.Generic;
using Terrain;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
	private const int TILESIZE = 10;
	private const int MAX_EDGES_IN_WATER = 1;

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

	[Header("Height")]
	[SerializeField]
	private int maxHeight;
	[SerializeField]
	private int minHeight;
	[SerializeField]
	private float heightCellDistanceModifier = 0.2f;


	public MapGenerationData Generate() //temp
	{
		//TODO make a courintiue with aniamting mesh movements when made for cool main menu effect
		return Generate(settings[0]);
	}

	public MapGenerationData Generate(MapGenerationSettings settings)
	{
		MapGenerationData mapGenerationData = new MapGenerationData(settings);
		PlaceMapSurface(mapGenerationData.settings);
		mapGenerationData.islands = CreateIslands(mapGenerationData.settings);
		return mapGenerationData;
	}

	private List<IslandGenerationData> CreateIslands(MapGenerationSettings settings)
	{
		Vector3 offset = MapGeneration.GetAreaOffset(settings.mapSize) * TILESIZE;
		List<IslandGenerationData> islandGenerationDatas = new List<IslandGenerationData>();
		List<Vector2> islandpoints = PoissonSampling.CreateRandomPoints(settings.mapSize * TILESIZE, settings.islandRadius);

		foreach (Vector2 islandpoint in islandpoints)
		{
			GameObject baseGameObject = PlaceIslandBase(settings, islandpoint, offset);
			IslandBase islandBase = baseGameObject.GetComponentInChildren<IslandBase>();
			IslandGenerationData islandData = CreateIsland(islandpoint, islandBase.islandSize, islandBase.islandPointRadius);
			islandData.islandBase = baseGameObject;
			PlaceIsland(settings, islandData, offset);
			islandGenerationDatas.Add(islandData);
		}
		foreach (IslandGenerationData islandpoint in islandGenerationDatas)
		{
			foreach (IslandBlock item in islandpoint.blocks)
			{
				item.gameObject.AddComponent<MeshCollider>().sharedMesh = item.GetComponent<MeshFilter>().mesh;
				item.gameObject.AddComponent<Surface>();
			}
		}
		return islandGenerationDatas;
	}

	private IslandGenerationData CreateIsland(Vector2 islandPoint, Vector2 area, float islandPointRadius)
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

	private void PlaceMapSurface(MapGenerationSettings settings)
	{
		Vector2 surfaceSize = settings.mapSize + new Vector2(settings.islandRadius / TILESIZE, settings.islandRadius / TILESIZE);
		GameObjectUtils.Clear(waterTransfrom);
		GameObjectUtils.Clear(groundTransfrom);
		GameObjectUtils.Clear(borderTransform);
		Vector3 mapOffset = MapGeneration.GetAreaOffset(surfaceSize) * TILESIZE;
		float tileRadius = TILESIZE / 2.0f;
		Vector3 offset = mapOffset - new Vector3(tileRadius, 0, tileRadius);
		for (int i = 0; i < surfaceSize.x; i++)
		{
			for (int j = 0; j < surfaceSize.y; j++)
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
				if (i == surfaceSize.x - 1)
				{
					Instantiate(borderprefab, water, Quaternion.Euler(0, 90, 0), borderTransform);
				}
				if (j == surfaceSize.y - 1)
				{
					Instantiate(borderprefab, water, Quaternion.Euler(0, 0, 0), borderTransform);
				}
			}
		}

	}

	private GameObject PlaceIslandBase(MapGenerationSettings settings, Vector3 island, Vector3 offset)
	{
		int baseIndex = UnityEngine.Random.Range(0, settings.bases.Count);
		float baseRotaion = UnityEngine.Random.Range(0, 360f);
		Vector3 basePosition = new Vector3(island.x, 0, island.y) - offset;
		return Instantiate(settings.bases[baseIndex], basePosition, Quaternion.Euler(0, baseRotaion, 0), islandsTransform);
	}

	private void PlaceIsland(MapGenerationSettings settings, IslandGenerationData island, Vector3 offset)
	{
		foreach (VoronoiCell voronoiCell in island.voronoiCells)
		{
			if (CanPlaceCell(voronoiCell, island.islandBase, settings))
			{
				IslandBase islandbaseChild = island.islandBase.GetComponentInChildren<IslandBase>();
				Vector3 cellPosition = island.islandBase.transform.position - MapGeneration.GetAreaOffset(islandbaseChild.islandSize) + voronoiCell.sitePos;
				float height = CalculateCellHeight(cellPosition, island.islandBase.transform.position, settings.islandRadius);
				IslandBlock mesh = CreateIslandBlock(island.islandBase.transform, voronoiCell, cellPosition, height);
				island.blocks.Add(mesh);
			}
		}

	}

	private bool CanPlaceCell(VoronoiCell voronoiCell, GameObject islandBase, MapGenerationSettings settings)
	{
		IslandBase islandbaseChild = islandBase.GetComponentInChildren<IslandBase>();
		int inWaterCount = 0;
		Vector3 sitePos = voronoiCell.sitePos - MapGeneration.GetAreaOffset(islandbaseChild.islandSize) + islandBase.transform.position;
		RayCastInfo<IslandBase> raycastHit = RayCastUtils.FindFirstObjectHit<IslandBase>(sitePos + new Vector3(0, 10, 0), Vector3.down);
		if (!raycastHit.focused || raycastHit.focusObject != islandbaseChild)
		{
			return false;
		}
		foreach (Vector3 borderCoord in voronoiCell.borderCoordinates)
		{
			Vector3 border = borderCoord - MapGeneration.GetAreaOffset(islandbaseChild.islandSize) + islandBase.transform.position;
			raycastHit = RayCastUtils.FindFirstObjectHit<IslandBase>(border + new Vector3(0, 10, 0), Vector3.down);
			if (!raycastHit.focused || raycastHit.focusObject != islandbaseChild)
			{
				inWaterCount++;
			}
		}

		return inWaterCount <= MAX_EDGES_IN_WATER;
	}

	private float CalculateCellHeight(Vector3 sitePos, Vector3 basePosition, float radius)
	{
		float distance = Vector3.Distance(sitePos, basePosition);
		float distanceHeight = (radius / distance) * heightCellDistanceModifier; //make 0.2 and 0.8 a modifier
		float perlinHeight = Mathf.PerlinNoise(sitePos.x * 300, sitePos.y * 300) * (maxHeight * (1 - heightCellDistanceModifier));
		float height = Mathf.Clamp(distanceHeight + perlinHeight, minHeight, maxHeight);
		return height;
	}

	private IslandBlock CreateIslandBlock(Transform parent, VoronoiCell voronoiCell, Vector3 position, float height)
	{
		GameObject cellGameObject = new GameObject("Cell");
		IslandBlock islandBlock = cellGameObject.AddComponent<IslandBlock>();
		cellGameObject.transform.parent = parent;
		cellGameObject.transform.position = position;

		MeshFilter meshFilter = cellGameObject.AddComponent<MeshFilter>();
		MeshRenderer meshRenderer = cellGameObject.AddComponent<MeshRenderer>();

		int materialIndex = UnityEngine.Random.Range(0, islandMaterials.Length);
		int h = (Mathf.RoundToInt(height - minHeight / (float)(maxHeight)) + materialIndex) / 2;

		int matindex = Mathf.Clamp(h, 0, islandMaterials.Length - 1);
		meshRenderer.material = islandMaterials[matindex];
		meshFilter.mesh = MapGeneration.CreateVoronoiMesh(voronoiCell, height);

		GameObject site = new GameObject("Site");
		site.transform.parent = cellGameObject.transform;
		site.transform.position = position + (Vector3.up * height);
		islandBlock.buildingSpawnPoint = site.transform;
		return islandBlock;
	}

	internal void ClearMap()
	{
		GameObjectUtils.Clear(waterTransfrom);
		GameObjectUtils.Clear(groundTransfrom);
		GameObjectUtils.Clear(borderTransform);
		GameObjectUtils.Clear(islandsTransform);
	}
}
