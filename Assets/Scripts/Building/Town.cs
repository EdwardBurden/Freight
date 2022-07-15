using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town : Building, IConsumer
{
	[SerializeField]
	private LandingPlatform platform;


	[SerializeField]
	private ResourceData[] accepts; //temp

	public ResourceData[] GetAcceptedResources()
	{
		return accepts;
	}

	public LandingPlatform GetLandingPlatform()
	{
		return platform;
	}

	public bool AcceptsResource(ResourceData resourceData)
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
}
