using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO
public class Market : Building, IConsumer, IProducer
{
	public ResourceData[] GetAcceptedResources()
	{
		throw new System.NotImplementedException();
	}

	public LandingPlatform GetLandingPlatform()
	{
		throw new System.NotImplementedException();
	}

	public ResourceData[] GetProducedResources()
	{
		throw new System.NotImplementedException();
	}

	public bool AcceptsResource(ResourceData resourceData)
	{
		throw new System.NotImplementedException();
	}

	public bool ProducesResource(ResourceData resourceData)
	{
		throw new System.NotImplementedException();
	}
}
