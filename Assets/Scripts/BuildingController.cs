using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Terrain;
using System;
using Random = UnityEngine.Random;

public class BuildingController : Singleton<BuildingController>
{
	public static event Action<Building> BuildingSelectedEvent;
	public static event Action<Building> BuildingDeSelectedEvent;

	[SerializeField]
	private GameObject[] buildingTest;

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
				int rotation = Random.Range(0, 360);
				GameObject building = Instantiate(buildingTest[index], islandGenerationData.blocks[i].transform.GetChild(0));
				building.transform.rotation = Quaternion.Euler(0, rotation, 0);
				building.transform.localPosition = Vector3.zero;
				buildings.Add(building.GetComponent<Building>());
			}
		}
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
}
