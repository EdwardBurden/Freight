using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;

public abstract class BuildingPanel<T> : Panel where T : Building
{
	[SerializeField]private TMP_Text nameField;

	protected T building;
	protected Storage buildingStorage;

	protected void OnEnable()
	{
		BuildingController.BuildingSelectedEvent += OnBuildingSelected;
		BuildingController.BuildingDeSelectedEvent += OnBuildingDeSelected;
	}

	private void OnDisable()
	{
		BuildingController.BuildingSelectedEvent -= OnBuildingSelected;
		BuildingController.BuildingDeSelectedEvent -= OnBuildingDeSelected;
	}

	protected virtual void OnBuildingDeSelected(Building building)
	{
		Hide();
	}

	protected virtual void OnBuildingSelected(Building building)
	{
		Hide();
		if (building is T)
		{
			this.building = building as T;
			Show();
		}
	}

	public override void Show()
	{
		base.Show();
		nameField.text = building.GetObjectName();
		buildingStorage = building.storage;
		if (buildingStorage != null)
		{
			buildingStorage.StorageUpdatedEvent += OnStorageUpdated;
		}
	}

	protected virtual void OnStorageUpdated()
	{
		//temp 
		Show();
	}

	public override void Hide()
	{
		base.Hide();
		if (buildingStorage != null)
		{
			buildingStorage.StorageUpdatedEvent -= OnStorageUpdated;
		}
	}
}
