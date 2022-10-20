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

	public bool CanProducesResource(ResourceDefintion resourceData)
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

	public void SellResourceToShip(ResourceDefintion resource, Ship ship)
	{
		throw new System.NotImplementedException();
		//TODO Whent the player is able to buy, there is stock of the resource, they get it for free since they placed the input resources
		//Update stockpile by removing one of resource taken
	}

	public void BuyResourceFromShip(ResourceDefintion resource, Ship ship)
	{
		throw new System.NotImplementedException();
		//TODO Factory should accept his resource without paying out anything
		//if they added resource then fufills the requirements trigger requirements fulfilled event, player can then start taking what is produced
		//update stockpile
	}
}
