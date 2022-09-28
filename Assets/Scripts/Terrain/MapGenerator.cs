using System.Collections.Generic;
using Terrain;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
	private const int BORDERBUFFER = 10;
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


	private MapGenerationData generationData;

	public MapGenerationData Generate() //temp
	{
		return Generate(settings[0]);
	}

	public MapGenerationData Generate(MapGenerationSettings settings)
	{
		generationData = CreateGenerationData(settings);
		InstatiateMapSurface(settings);
		InstatiateIslands(settings, generationData);
		return generationData;
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
		Vector2 surfaceSize = settings.mapSize + new Vector2(settings.islandRadius/ TILESIZE, settings.islandRadius / TILESIZE);
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

	private void InstatiateIslands(MapGenerationSettings settings, MapGenerationData data)
	{
		Vector3 offset = GetMapCenterOffset(settings);
		foreach (IslandGenerationData island in data.islands)
		{
			int baseIndex = UnityEngine.Random.Range(0, settings.bases.Count);
			float baseRotaion = UnityEngine.Random.Range(0, 360f);
			Vector3 basePosition = new Vector3(island.center.x, 0, island.center.y) - offset;
			GameObject islandbase = Instantiate(settings.bases[baseIndex], basePosition, Quaternion.Euler(0, baseRotaion, 0), islandsTransform);

			foreach (VoronoiCell voronoiCell in island.voronoiCells)
			{
				Vector3 sitePos = voronoiCell.sitePos - MapGeneration.GetAreaOffset(settings.islandSize) + basePosition;
				if (CanPlaceCell(voronoiCell, islandbase, settings))
				{
					float distance = Vector3.Distance(sitePos, basePosition);
					GameObject mesh = DrawMesh(settings, islandbase.transform, voronoiCell, basePosition, distance);
					island.cells.Add(mesh);
				}
			}
		}
	}

	private bool CanPlaceCell(VoronoiCell voronoiCell, GameObject islandBase, MapGenerationSettings settings)
	{
		int inWaterCount = 0;
		Vector3 sitePos = voronoiCell.sitePos - MapGeneration.GetAreaOffset(settings.islandSize) + islandBase.transform.position;
		RayCastInfo<IslandBase> raycastHit = RayCastUtils.FindFirstObjectHit<IslandBase>(sitePos + new Vector3(0, 10, 0), Vector3.down);
		if (!raycastHit.focused || raycastHit.focusObject != islandBase.GetComponentInChildren<IslandBase>())
		{
			return false;
		}
		foreach (Vector3 borderCoord in voronoiCell.borderCoordinates)
		{
			Vector3 border = borderCoord - MapGeneration.GetAreaOffset(settings.islandSize) + islandBase.transform.position;
			raycastHit = RayCastUtils.FindFirstObjectHit<IslandBase>(border + new Vector3(0, 10, 0), Vector3.down);
			if (!raycastHit.focused || raycastHit.focusObject != islandBase.GetComponentInChildren<IslandBase>())
			{
				inWaterCount++;
			}
		}

		return inWaterCount <= MAX_EDGES_IN_WATER;
	}



	private GameObject DrawMesh(MapGenerationSettings settings, Transform parent, VoronoiCell voronoiCell, Vector3 center, float distance)
	{
		GameObject cellGameObject = new GameObject("Cell");

		cellGameObject.transform.parent = parent;
		cellGameObject.transform.position = center - MapGeneration.GetAreaOffset(settings.islandSize) + voronoiCell.sitePos;

		int materialIndex = UnityEngine.Random.Range(0, islandMaterials.Length);
		float distanceHeight = Mathf.Min(5, (settings.islandRadius / distance) * 0.2f);
		float perlinHeight = Mathf.PerlinNoise(voronoiCell.sitePos.x * 300, voronoiCell.sitePos.y * 300) * 10;
		int height = Mathf.CeilToInt(distanceHeight + perlinHeight);

		MeshFilter meshFilter = cellGameObject.AddComponent<MeshFilter>();
		MeshRenderer meshRenderer = cellGameObject.AddComponent<MeshRenderer>();
		meshRenderer.material = islandMaterials[materialIndex];
		meshFilter.mesh = MapGeneration.CreateVoronoiMesh(voronoiCell, height);

		GameObject site = new GameObject("Site");
		site.transform.parent = cellGameObject.transform;
		site.transform.position = center - MapGeneration.GetAreaOffset(settings.islandSize) + voronoiCell.sitePos + (Vector3.up * height);


		return cellGameObject;
	}

	private Vector3 GetMapCenterOffset(MapGenerationSettings mapGenerationSettings)
	{
		return MapGeneration.GetAreaOffset(mapGenerationSettings.mapSize) * TILESIZE;
	}
}
