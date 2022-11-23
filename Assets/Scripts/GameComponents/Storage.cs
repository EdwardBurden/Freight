using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Storage : MonoBehaviour
{
	//put storage ui update here
	public event Action StorageUpdatedEvent;

	public Dictionary<ResourceDefintion, int> storage = new Dictionary<ResourceDefintion, int>(); //simple

	public void RemoveStorage(ResourceDefintion resourceDefintion, int amount = 1)
	{
		if (storage.ContainsKey(resourceDefintion) && storage[resourceDefintion] > 0)
		{
			storage[resourceDefintion] -= amount;
			StorageUpdatedEvent?.Invoke();
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
