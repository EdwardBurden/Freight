using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO
public class Market : Building, IConsumer, IProducer
{
	public ResourceDefintion[] GetAcceptedResources()
	{
		throw new System.NotImplementedException();
	}

	public LandingPlatform GetProducerLandingPlatform()
	{
		throw new System.NotImplementedException();
	}

	public ResourceDefintion[] GetProducedResources()
	{
		throw new System.NotImplementedException();
	}

	public bool AcceptsResource(ResourceDefintion resourceData)
	{
		throw new System.NotImplementedException();
	}

	public bool ProducesResource(ResourceDefintion resourceData)
	{
		throw new System.NotImplementedException();
	}

	public LandingPlatform GetConsumerLandingPlatform()
	{
		throw new System.NotImplementedException();
	}
}
