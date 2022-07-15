using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryObject : MonoBehaviour //goes on ship for carrying stuff
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

	public bool HasFreightOfResource(ResourceData resourceData)
	{
		return GetAllFreight().Find(x => x.resourceData == resourceData) != null;
	}

	public bool HasStorageSpace()
	{
		return GetFirstEmptyStorageTransform() != null;
	}

	public FreightObject GetFreightOfResource(ResourceData resourceData) //use to add to platform postion before landing at platforms
	{
		FreightObject freightObject = GetAllFreight().Find(x => x.resourceData == resourceData);
		return freightObject;
	}

	public Transform GetFirstStorageTransformOfResource(ResourceData resourceData) //use to add to platform postion before landing at platforms
	{
		FreightObject freightObject = GetAllFreight().Find(x => x.resourceData == resourceData);
		return freightObject.transform.parent.transform;
	}

	public Transform GetFirstEmptyStorageTransform() //use to add to platform postion before landing at platforms
	{
		foreach (var item in storageTransforms)
		{
			if (item.childCount == 0)
			{
				return item.transform;
			}
		}
		return null;
	}

}
