using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Terrain;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.PlayerSettings;
using static UnityEditor.Progress;

public class MapGenerator : MonoBehaviour
{
	private const int TILESIZE = 10;
	private const int MAX_EDGES_IN_WATER = 1;
	[SerializeReference] public LayerMask unnavigableLayer;
	[Header("Map Surface")]
	[SerializeField] private GameObject waterPrefab;
	[SerializeField] private GameObject groundPrefab;
	[SerializeField] private GameObject borderprefab;
	[SerializeField] private Transform waterTransfrom;
	[SerializeField] private Transform groundTransfrom;
	[SerializeField] private Transform borderTransform;
	[SerializeField] private Transform islandsTransform;
	[SerializeField] private int waterHeight;

	[Header("island Generation")]
	[SerializeField] private Material[] islandMaterials;

	[Header("Height")]
	[SerializeField] private int maxHeight;
	[SerializeField] private int minHeight;
	[SerializeField] private float heightCellDistanceModifier = 0.2f;

	public MapGenerationData GenerateMapData(MapGenerationSettings settings)
	{
		//Create Random Points
		Vector2 islandGenerateSize = settings.mapSize * TILESIZE;
		Vector2 blockgenerateSize = (settings.mapSize * TILESIZE) + new Vector2(settings.islandRadius, settings.islandRadius);
		Vector3 islandOffset = -MapGeneration.GetAreaOffset(settings.mapSize) * TILESIZE;
		Vector3 blockOffset = -(MapGeneration.GetAreaOffset(settings.mapSize) * TILESIZE) - (new Vector3(settings.islandRadius, 0, settings.islandRadius) / 2);
		List<Vector2> islandpoints = PoissonSampling.CreateRandomPoints(islandGenerateSize, settings.islandRadius, islandOffset);
		List<Vector2> blockPoints = PoissonSampling.CreateRandomPoints(blockgenerateSize, settings.blockRadius, blockOffset);
		List<Vector2>[] blockPointsToIslands = new List<Vector2>[islandpoints.Count];

		//Assign block points to island point
		float islandRadius = settings.islandRadius * settings.islandRadius;
		foreach (Vector2 blockPoint in blockPoints)
		{
			bool canAdd = false;
			float closest = 100000;
			int index = 0;
			for (int i = 0; i < islandpoints.Count; i++)
			{
				float sqrMagnitude = (islandpoints[i] - blockPoint).sqrMagnitude;
				if (sqrMagnitude < islandRadius)
				{
					canAdd = true;
					if (sqrMagnitude < closest)
					{
						closest = sqrMagnitude;
						index = i;
					}
				}
			}
			if (canAdd)
			{
				if (blockPointsToIslands[index] == null)
				{
					blockPointsToIslands[index] = new List<Vector2>();
				}
				blockPointsToIslands[index].Add(blockPoint);
			}
		}

		//create cells
		List<IslandGenerationData> islandGenerations = new List<IslandGenerationData>();
		for (int i = 0; i < blockPointsToIslands.Length; i++)
		{
			List<Vertex> boundarypoints = blockPointsToIslands[i].ToVertices();
			List<Vertex> vertices = blockPointsToIslands[i].ToVertices();
			List<Vertex> boundary = JarvisMarchAlgorithm.GetConvexHull(boundarypoints);
			List<VoronoiCell> cells = MapGeneration.CreateVoronoiCells(vertices);
			MapGeneration.ClipVoronoiDiagram(cells, boundary);
			cells = MapGeneration.CreateTrianglesFromVoronoiCells(cells);
			IslandGenerationData islandGenerationData = new IslandGenerationData(islandpoints[i], cells);
			islandGenerations.Add(islandGenerationData);
		}

		//return data
		MapGenerationData mapGenerationData = new MapGenerationData(islandGenerations);
		return mapGenerationData;
	}

	public void PlaceMapSurface(MapGenerationSettings settings)
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
		GameObject waterobject = Instantiate(waterPrefab, Vector3.up * waterHeight, Quaternion.identity, waterTransfrom);
		GameObject groundobject = Instantiate(groundPrefab, Vector3.zero, Quaternion.identity, groundTransfrom);
	}

	public GameObject PlaceIslandBase(MapGenerationSettings settings, Vector3 island)
	{
		//Vector3 islandOffset = -MapGeneration.GetAreaOffset(settings.mapSize) * TILESIZE;
		int baseIndex = UnityEngine.Random.Range(0, settings.bases.Count);
		float baseRotaion = UnityEngine.Random.Range(0, 360f);
		Vector3 basePosition = new Vector3(island.x, 0, island.y);// + //islandOffset;
		return Instantiate(settings.bases[baseIndex], basePosition, Quaternion.Euler(0, baseRotaion, 0), islandsTransform);
	}

	public bool CanPlaceCell(VoronoiCell voronoiCell, GameObject islandBase)
	{
		IslandBase islandbaseChild = islandBase.GetComponentInChildren<IslandBase>();
		int inWaterCount = 0;
		RayCastInfo<IslandBase> raycastHit = RayCastUtils.FindFirstObjectHit<IslandBase>(voronoiCell.sitePos + new Vector3(0, 10, 0), Vector3.down);
		if (!raycastHit.focused || raycastHit.focusObject != islandbaseChild)
		{
			return false;
		}
		foreach (Vector3 borderCoord in voronoiCell.borderCoordinates)
		{
			raycastHit = RayCastUtils.FindFirstObjectHit<IslandBase>(borderCoord + new Vector3(0, 10, 0), Vector3.down);
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

	public IslandBlock CreateIslandBlock(Transform parent, VoronoiCell voronoiCell, MapGenerationSettings settings)
	{
		//Create Object
		GameObject cellGameObject = new GameObject("Cell");
		IslandBlock islandBlock = cellGameObject.AddComponent<IslandBlock>();
		Surface surface = cellGameObject.AddComponent<Surface>();
		cellGameObject.transform.parent = parent;
		cellGameObject.transform.position = voronoiCell.sitePos;
		cellGameObject.layer = 7;//unnavigableLayer

		//Create Mesh
		MeshFilter meshFilter = cellGameObject.AddComponent<MeshFilter>();
		MeshRenderer meshRenderer = cellGameObject.AddComponent<MeshRenderer>();
		MeshCollider meshCollider = cellGameObject.AddComponent<MeshCollider>();
		float height = CalculateCellHeight(voronoiCell.sitePos, parent.position, settings.islandRadius);
		int materialIndex = UnityEngine.Random.Range(0, islandMaterials.Length);
		meshRenderer.material = islandMaterials[materialIndex];
		meshFilter.mesh = MapGeneration.CreateVoronoiMesh(voronoiCell, height);
		meshCollider.sharedMesh = meshFilter.mesh;
		meshCollider.convex = true;

		//object
		GameObject site = new GameObject("Site");
		site.transform.parent = cellGameObject.transform;
		site.transform.position = voronoiCell.sitePos + (Vector3.up * height);
		islandBlock.buildingSpawnPoint = site.transform;
		islandBlock.resourceGroundType = (ResourceGroundType)materialIndex;
		islandBlock.sitePostion = voronoiCell.sitePos;
		return islandBlock;
	}

	public Vector3 FindCentralEmptyGridPostion(MapGenerationSettings settings) //could put in spawncontroller
	{
		Vector3 mapOffset = MapGeneration.GetAreaOffset(settings.mapSize) * TILESIZE;
		Vector2 islandGenerateSize = settings.mapSize * TILESIZE;
		int baseSize = 5;
		Vector2 baseGridSize = islandGenerateSize / baseSize;
		List<Vector3> possibleLocations = new List<Vector3>();
		for (int x = 0; x < baseGridSize.x; x++)
		{
			for (int z = 0; z < baseGridSize.y; z++)
			{
				Vector3 pos = new Vector3(x, 0, z) * baseSize;
				if (!Physics.CheckBox(pos - mapOffset, Vector3.one * baseSize, Quaternion.identity, unnavigableLayer))
				{
					possibleLocations.Add(pos - mapOffset);
				}
			}
		}
		Vector3 finalPos = possibleLocations.OrderBy(x => (x - this.gameObject.transform.position).sqrMagnitude).FirstOrDefault();
		return finalPos;
	}

	internal void ClearMap()
	{
		GameObjectUtils.Clear(waterTransfrom);
		GameObjectUtils.Clear(groundTransfrom);
		GameObjectUtils.Clear(borderTransform);
		GameObjectUtils.Clear(islandsTransform);
	}
}
