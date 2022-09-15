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

	public Material[] materials;

	private MapGenerationData generationData = new MapGenerationData();


	private void Start()
	{
		Generate(settings[0]);//test
	}

	private Vector3 GetMapCenterOffset(MapGenerationSettings mapGenerationSettings)
	{
		return MapGeneration.GetAreaOffset(mapGenerationSettings.mapSize) * TILESIZE;
	}

	public void Generate(MapGenerationSettings mapGenerationSettings)
	{
		generationData = CreateGenerationData(mapGenerationSettings);
		InstatiateMapSurface(mapGenerationSettings);
		InstatiateIslands(mapGenerationSettings, generationData);
	}

	private void InstatiateIslands(MapGenerationSettings mapGenerationSettings, MapGenerationData mapGenerationData)
	{
		Vector3 offset = GetMapCenterOffset(mapGenerationSettings);
		foreach (IslandGenerationData island in mapGenerationData.islandGenerationDatas)
		{
			Vector3 worldSpacepoint = new Vector3(island.center.x, 0, island.center.y) - offset;
			GameObject islandbase = Instantiate(pointPrefab, worldSpacepoint, Quaternion.Euler(0, 0, 0), islandsTransform);
			/*foreach (Vector2 spot in island.spots)
			{
				GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
				Vector3 spot3d = new Vector3(spot.x, 4, spot.y) - MapGeneration.GetAreaOffset(mapGenerationSettings.islandSize);
				gameObject.transform.position = spot3d + worldSpacepoint;
			}*/
			foreach (var item in island.voronoiCells)
			{
				GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
				Vector3 spot3d = item.sitePos - MapGeneration.GetAreaOffset(mapGenerationSettings.islandSize);
				gameObject.transform.position = spot3d + worldSpacepoint;
				InteractionUtils.RayCastInfo<IslandBase> raycastHit = InteractionUtils.FindFirstObjectHit<IslandBase>(spot3d + worldSpacepoint + new Vector3(0,10,0), Vector3.down);
				if (raycastHit.focused && raycastHit.focusObject == islandbase.GetComponentInChildren<IslandBase>())
				{
					DrawMesh(islandbase.transform, item, island.center.ToVector3());
				}
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
		List<Vector2> islandpoints = PoissonSampling.CreateRandomPoints(area, spotRadius);
		List<Vertex> boundarypoints = islandpoints.ToVertices();
		List<Vertex> verices = islandpoints.ToVertices();
		islandGenerationData.boundary = JarvisMarchAlgorithm.GetConvexHull(boundarypoints);
		islandGenerationData.voronoiCells = MapGeneration.CreateVoronoiCells(verices);
		MapGeneration.ClipVoronoiDiagram(islandGenerationData.voronoiCells, islandGenerationData.boundary);
		islandGenerationData.voronoiCells = MapGeneration.CreateTrianglesFromVoronoiCells(islandGenerationData.voronoiCells);


		islandGenerationData.spots = islandpoints;
		return islandGenerationData;
	}

	private void InstatiateMapSurface(MapGenerationSettings mapGenerationSettings)
	{
		GameObjectUtils.Clear(waterTransfrom);
		GameObjectUtils.Clear(groundTransfrom);
		GameObjectUtils.Clear(borderTransform);

		float xOffset = (mapGenerationSettings.mapSize.x / 2.0f) * TILESIZE;
		float yOffset = (mapGenerationSettings.mapSize.y / 2.0f) * TILESIZE;
		float tileRadius = TILESIZE / 2.0f;
		Vector3 offset = new Vector3(xOffset - tileRadius, 0, yOffset - tileRadius);
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
	private void DrawMesh(Transform parent, VoronoiCell voronoiCell, Vector3 center)
	{
		GameObject gameObject = new GameObject("Cell");

		int h = UnityEngine.Random.Range(0, materials.Length);

		//	float dist = Vector2.Distance(voronoiCell.sitePos.xz(), Vector2.zero);
		//	int height = Random.Range(1, 5);
		float sample = Mathf.PerlinNoise(voronoiCell.sitePos.x * 300, voronoiCell.sitePos.y * 300) * 10;
		Mesh mesh = MapGeneration.CreateVoronoiMesh(voronoiCell, Mathf.CeilToInt(sample) + 2);

		MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
		MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
		meshRenderer.material = materials[h];
		gameObject.transform.parent = parent;
		gameObject.transform.position = center - MapGeneration.GetAreaOffset(settings[0].islandSize) - GetMapCenterOffset(settings[0]);

		meshFilter.mesh = mesh;
	}



	private void OnDrawGizmos()
	{

		int nextvert = 1;
		//IslandGenerationData islandGeneration = generationData.islandGenerationDatas[0];
		foreach (IslandGenerationData islandGeneration in generationData.islandGenerationDatas)
		{


			Gizmos.color = Color.blue;
			for (int i = 0; i < islandGeneration.boundary.Count; i++, nextvert++)
			{
				if (nextvert == islandGeneration.boundary.Count)
				{
					nextvert = 0;
				}
				Vector3 start = new Vector3(0, 3, 0) + islandGeneration.boundary[i].position + islandGeneration.center.ToVector3() - MapGeneration.GetAreaOffset(settings[0].islandSize) - GetMapCenterOffset(settings[0]);
				Vector3 end = new Vector3(0, 3, 0) + islandGeneration.boundary[nextvert].position + islandGeneration.center.ToVector3() - MapGeneration.GetAreaOffset(settings[0].islandSize) - GetMapCenterOffset(settings[0]);
				Gizmos.DrawLine(start, end);
			}



			Gizmos.color = Color.red;
			for (int i = 0; i < islandGeneration.voronoiCells.Count; i++)
			{

				for (int j = 0; j < islandGeneration.voronoiCells[i].edges.Count; j++)
				{
					Vector3 start = new Vector3(0, 4, 0) + islandGeneration.voronoiCells[i].edges[j].v1 + islandGeneration.center.ToVector3() - MapGeneration.GetAreaOffset(settings[0].islandSize) - GetMapCenterOffset(settings[0]);
					Vector3 end = new Vector3(0, 4, 0) + islandGeneration.voronoiCells[i].edges[j].v2 + islandGeneration.center.ToVector3() - MapGeneration.GetAreaOffset(settings[0].islandSize) - GetMapCenterOffset(settings[0]);
					Gizmos.DrawLine(start, end);
				}
			}
			Gizmos.color = Color.green;
			foreach (var item in islandGeneration.voronoiCells)
			{
				int nextCellPoint = 1;
				//Gizmos.DrawSphere(item.sitePos, 1);

				Gizmos.DrawSphere(item.sitePos + islandGeneration.center.ToVector3() - MapGeneration.GetAreaOffset(settings[0].islandSize) - GetMapCenterOffset(settings[0]), 2);
				for (int i = 0; i < item.borderCoordinates.Count; i++, nextCellPoint++)
				{
					//Gizmos.DrawSphere(item.borderCoordinates[i], 0.5f);
					if (nextCellPoint == item.borderCoordinates.Count)
					{
						nextCellPoint = 0;
					}
					Vector3 start = new Vector3(0, 5, 0) + item.borderCoordinates[i] + islandGeneration.center.ToVector3() - MapGeneration.GetAreaOffset(settings[0].islandSize) - GetMapCenterOffset(settings[0]);
					Vector3 end = new Vector3(0, 5, 0) + item.borderCoordinates[nextCellPoint] + islandGeneration.center.ToVector3() - MapGeneration.GetAreaOffset(settings[0].islandSize) - GetMapCenterOffset(settings[0]);
					Gizmos.DrawLine(start, end);
				}
			}
		}


	}
}
