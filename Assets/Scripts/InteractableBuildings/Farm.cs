using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : Building, IProducer
{
	[SerializeField]
	private GameObject platform;

	[SerializeField]
	private ResourceData[] produces; //temp

	public GameObject GetLandingPlatform()
	{
		throw new System.NotImplementedException();
	}

	public Vector3 GetLandingPosition()
	{
		return platform.transform.position;
	}

	public Quaternion GetLandingRotation()
	{
		return platform.transform.rotation;
	}

	public ResourceData[] GetProducedResources()
	{
		return produces;
	}
}
