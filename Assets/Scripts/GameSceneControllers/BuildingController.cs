using System.Collections.Generic;
using UnityEngine;
using Terrain;
using System;
using Random = UnityEngine.Random;
using System.Linq;

[Serializable]
public class BuildingSpawnInfo
{
	public BuildingDefintion building;
	public float spawnChance;
	public int minSpawnAmount;
	public int maxSpawnAmount;
}

public class BuildingController : Singleton<BuildingController>
{
	public static event Action<Building> BuildingSelectedEvent;
	public static event Action<Building> BuildingDeSelectedEvent;

	[SerializeField]
	private BuildingSpawnInfo[] builindSpawnInfos;

	[SerializeField]
	private BuildingDefintion empty;

	private Building buildingSelected;

	private List<Building> buildings = new List<Building>();

	private void Awake()
	{
		_instance = this;
	}

	public void OnEnable()
	{
		MouseInput.LeftClickNonInteractableEvent += DeSelectBuilding;
		MouseInput.LeftClickInteractableEvent += OnLeftClickInteractable;
	}

	public void OnDisable()
	{
		MouseInput.LeftClickNonInteractableEvent -= DeSelectBuilding;
		MouseInput.LeftClickInteractableEvent -= OnLeftClickInteractable;
	}

	private void DeSelectBuilding()
	{
		if (buildingSelected != null)
		{
			buildingSelected = null;
			BuildingDeSelectedEvent?.Invoke(buildingSelected);
		}
	}

	private void OnLeftClickInteractable(InteractableObject obj, Vector3 position)
	{
		Building building = obj.GetComponent<Building>();
		DeSelectBuilding();
		if (building != null)
		{
			SelectBuilding(building);
		}
	}

	private void SelectBuilding(Building building)
	{
		this.buildingSelected = building;
		BuildingSelectedEvent?.Invoke(buildingSelected);
	}

	public void DistributeBuildings(MapGenerationData mapGenerationData)
	{
		List<IslandBlock> islandBlocks = mapGenerationData.islands.SelectMany(x => x.blocks).OrderBy(x => Guid.NewGuid()).ToList(); //TODO Remove LINQ

		foreach (BuildingSpawnInfo buildingSpawnInfo in builindSpawnInfos)
		{
			int amount = Random.Range(buildingSpawnInfo.minSpawnAmount, buildingSpawnInfo.maxSpawnAmount);
			for (int i = 0; i < amount; i++)
			{
				AddBuilding(buildingSpawnInfo.building, islandBlocks[0]); //TODO improve so no index error if not enough space
				islandBlocks.RemoveAt(0);
			}
		}

		foreach (var item in islandBlocks)
		{
			bool placed = false;
			foreach (BuildingSpawnInfo buildingSpawnInfo in builindSpawnInfos)
			{
				if (!placed && Random.Range(0f, 1f) < buildingSpawnInfo.spawnChance)
				{
					AddBuilding(buildingSpawnInfo.building, item);
					placed = true;
				}
			}
			if (!placed)
			{
				AddBuilding(empty, item);
			}
		}
	}

	private Building AddBuilding(BuildingDefintion buildingToPlace, IslandBlock islandBlock)
	{
		int rotation = Random.Range(0, 360);
		Building building = Instantiate(buildingToPlace.buildingPrefab, islandBlock.buildingSpawnPoint);
		building.islandBlock = islandBlock;
		building.transform.rotation = Quaternion.Euler(0, rotation, 0);
		building.transform.localPosition = Vector3.zero;
		building.Init(buildingToPlace);
		buildings.Add(building);
		return building;
	}

	public IEnumerable<IConsumer> GetAllConsumerBuildings()
	{
		List<IConsumer> consumers = new List<IConsumer>();
		for (int i = 0; i < buildings.Count; i++)
		{
			if (buildings[i] is IConsumer consumer)
			{
				consumers.Add(consumer);
			}
		}
		return consumers;
	}

	public Building ReplaceBuilding(Building currentbuilding, BuildingDefintion selectedBuilding)
	{
		IslandBlock islandBlock = currentbuilding.islandBlock;
		GameObject.Destroy(currentbuilding.gameObject);
		buildings.Remove(currentbuilding);
		Building newBuilding = AddBuilding(selectedBuilding, islandBlock);
		return newBuilding;
	}

}
