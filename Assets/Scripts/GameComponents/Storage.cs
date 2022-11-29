using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UI;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Storage : MonoBehaviour
{
	//put storage ui update here
	public event Action StorageUpdatedEvent;

	[SerializeField] private Transform storageMessageTransform;

	public Dictionary<ResourceDefintion, int> storage = new Dictionary<ResourceDefintion, int>(); //simple

	[SerializeField]
	public DisplayMessage_InCamera storageUpdatePrefab;

	public void RemoveStorage(ResourceDefintion resourceDefintion, int amount = 1)
	{
		if (storage.ContainsKey(resourceDefintion) && storage[resourceDefintion] > 0)
		{
			storage[resourceDefintion] -= amount;
			StorageUpdatedEvent?.Invoke();
			DisplayMessage_InCamera displayMessage_InCamera = GameUIController.instance.CreateInScene(storageUpdatePrefab, storageMessageTransform) as DisplayMessage_InCamera;
			displayMessage_InCamera.ShowTransactionText($"Storage of {amount} {resourceDefintion.resourceName}'s removed", 5);
		}
	}

	public void AddStorage(ResourceDefintion resourceDefintion, int amount = 1)
	{
		if (storage.ContainsKey(resourceDefintion))
		{
			storage[resourceDefintion] += amount;
		}
		else
		{
			storage.Add(resourceDefintion, amount);
		}
		StorageUpdatedEvent?.Invoke();
		DisplayMessage_InCamera displayMessage_InCamera = GameUIController.instance.CreateInScene(storageUpdatePrefab, storageMessageTransform) as DisplayMessage_InCamera;
		displayMessage_InCamera.ShowTransactionText($"Storage of {amount} {resourceDefintion.resourceName}'s added" , 5);
	}

	public bool HasStorage(ResourceDefintion resourceData)
	{
		return GetStorage(resourceData) > 0;

	}

	public int GetStorage(ResourceDefintion resourceData)
	{
		if (storage.ContainsKey(resourceData))
		{
			return storage[resourceData];
		}
		return 0;
	}

	public void ClearStorage()
	{
		storage.Clear();
		StorageUpdatedEvent?.Invoke();
	}
}
