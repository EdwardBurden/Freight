using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town : Building, IConsumer
{
	[SerializeField]
	private LandingPlatform platform;


	[SerializeField]
	private ResourceDefintion[] accepts;

	public ResourceDefintion[] GetAcceptedResources()
	{
		return accepts;
	}

	public bool AcceptsResource(ResourceDefintion resourceData)
	{
		for (int i = 0; i < accepts.Length; i++)
		{
			if (accepts[i] == resourceData)
			{
				return true;
			}
		}
		return false;
	}

	public LandingPlatform GetConsumerLandingPlatform()
	{
		return platform;
	}
}
