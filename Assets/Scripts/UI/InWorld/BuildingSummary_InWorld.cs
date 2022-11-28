using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class BuildingSummary_InWorld : MonoBehaviour
{
	[SerializeField] protected TMP_Text buildingNameText;
	protected Building building;


	public void Init(Building building)
	{
		this.building = building;
		if (building.storage != null)
		{
			building.storage.StorageUpdatedEvent += OnStorageUpdatedEvent;
		}
		Show();
	}

	private void OnStorageUpdatedEvent()
	{

	}

	protected virtual void Show()
	{
		buildingNameText.text = building.GetObjectName();

	}

	public void OnHover()
	{
		Show();
		//throw new NotImplementedException();
	}

	public void OffHover()
	{
		Show();
		//throw new NotImplementedException();
	}

	private void OnDestroy()
	{
		if (building.storage != null)
		{
			building.storage.StorageUpdatedEvent -= OnStorageUpdatedEvent;
		}
	}
}
