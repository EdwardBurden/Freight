using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryStorage : MonoBehaviour
{
	public Transform[] physicalStorageTransforms;

	public bool HasSpace()
	{
		foreach (Transform storage in physicalStorageTransforms)
		{
			if (!storage.GetComponentInChildren<Freight>())
			{
				return true;
			}
		}
		return false;
	}

	public Transform GetAvailableStorageTransform()
	{
		foreach (Transform storage in physicalStorageTransforms)
		{
			if (!storage.GetComponentInChildren<Freight>())
			{
				return storage;
			}
		}
		return null;
	}

	public Freight SelectFirstFreight() //temp and t be replaced
	{
		foreach (Transform storage in physicalStorageTransforms)
		{
			Freight feight = storage.GetComponentInChildren<Freight>();
			if (feight != null)
			{
				return feight;
			}
		}
		return null;
	}

}
