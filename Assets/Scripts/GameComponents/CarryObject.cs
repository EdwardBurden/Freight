using System.Collections.Generic;
using UnityEngine;

public class CarryObject : MonoBehaviour
{
	[SerializeField]
	private Transform[] storageTransforms;

	public List<FreightObject> GetAllFreight()
	{
		List<FreightObject> freightObjects = new List<FreightObject>();
		foreach (Transform storage in storageTransforms)
		{
			if (storage.childCount == 1)
			{
				freightObjects.Add(storage.GetChild(0).GetComponent<FreightObject>());
			}
		}
		return freightObjects;
	}

	public bool HasFreightOfResource(ResourceDefintion resourceData)
	{
		return GetAllFreight().Find(x => x.resourceData == resourceData) != null;
	}

	public bool HasStorageSpace()
	{
		return GetFirstEmptyStorageTransform() != null;
	}

	public FreightObject GetFreightOfResource(ResourceDefintion resourceData)
	{
		FreightObject freightObject = GetAllFreight().Find(x => x.resourceData == resourceData);
		return freightObject;
	}

	public Transform GetFirstStorageTransformOfResource(ResourceDefintion resourceData)
	{
		FreightObject freightObject = GetAllFreight().Find(x => x.resourceData == resourceData);
		return freightObject.transform.parent.transform;
	}

	public Transform GetFirstEmptyStorageTransform()
	{
		foreach (Transform transform in storageTransforms)
		{
			if (transform.childCount == 0)
			{
				return transform.transform;
			}
		}
		return null;
	}
}
