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

	public bool ProducesResource(ResourceData resourceData)
	{
		for (int i = 0; i < produces.Length; i++)
		{
			if (produces[i] == resourceData)
			{
				return true;
			}
		}
		return false;
	}

}
