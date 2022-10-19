using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Terrain;
using System;
using Random = UnityEngine.Random;
using System.Transactions;

public class BuildingController : Singleton<BuildingController>
{
	public static event Action<Building> BuildingSelectedEvent;
	public static event Action<Building> BuildingDeSelectedEvent;

	[SerializeField]
	private Building[] buildingTest;

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
		foreach (IslandGenerationData islandGenerationData in mapGenerationData.islands)
		{
			for (int i = 0; i < islandGenerationData.blocks.Count; i++)
			{
				int index = Random.Range(0, buildingTest.Length);
				AddBuilding(buildingTest[index], islandGenerationData.blocks[i]);
			}
		}
	}

	private Building AddBuilding(Building buildingToPlace, IslandBlock islandBlock)
	{
		int rotation = Random.Range(0, 360);
		Building building = Instantiate(buildingToPlace, islandBlock.buildingSpawnPoint);
		building.islandBlock = islandBlock;
		building.transform.rotation = Quaternion.Euler(0, rotation, 0);
		building.transform.localPosition = Vector3.zero;
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

	public Building ReplaceBuilding(Building currentbuilding, Building selectedBuilding)
	{
		IslandBlock islandBlock = currentbuilding.islandBlock;
		GameObject.Destroy(currentbuilding.gameObject);
		buildings.Remove(currentbuilding);
		Building newBuilding = AddBuilding(selectedBuilding, islandBlock);
		return newBuilding;
	}
}
