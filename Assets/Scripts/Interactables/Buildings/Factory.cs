using System.Collections.Generic;
using UnityEngine;

public class Factory : Building, IProducer, IConsumer
{
	[SerializeField]
	private LandingPlatform inputPlatform;

	[SerializeField]
	private LandingPlatform outputPlatform;

	[SerializeField]
	private ResourceDefintion[] produces;

	[SerializeField]
	private ResourceDefintion[] accepts;


	private SettlementWants consumerWants;

	public ResourceDefintion[] GetAcceptedResources()
	{
		return accepts;
	}

	public ResourceDefintion[] GetProducedResources()
	{
		return produces;
	}

	public LandingPlatform GetProducerLandingPlatform()
	{
		return outputPlatform;
	}

	public LandingPlatform GetConsumerLandingPlatform()
	{
		return inputPlatform;
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

	public bool ProducesResource(ResourceDefintion resourceData)
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

	public void SetAcceptedResources(ResourceDefintion[] resourceDefintions)
	{
		this.accepts = resourceDefintions;
	}

	public SettlementWants GetWants()
	{
		return consumerWants;
	}

	public void SetWants(SettlementWants consumerWants)
	{
		this.consumerWants = consumerWants;
	}
}
