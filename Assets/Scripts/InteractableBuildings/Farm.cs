using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : Building, IProducer
{
	[SerializeField]
	private ResourceData[] produces; //temp

	public Vector3 GetLandingPosition()
	{
		return this.transform.position;
	}

	public ResourceData[] GetProducedResources()
	{
		return produces;
	}
}
