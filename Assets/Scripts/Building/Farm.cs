using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : Building, IProducer
{
	[SerializeField]
	private LandingPlatform platform;

	[SerializeField]
	private ResourceData[] produces; //temp

	public LandingPlatform GetLandingPlatform()
	{
		return platform;
	}

	public ResourceData[] GetProducedResources()
	{
		return produces;
	}
}
