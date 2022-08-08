using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO
public class Market : Building, IConsumer, IProducer
{
	[SerializeField]
	private float buyPriceModifier;//temp
	[SerializeField]
	private float sellPriceModifier;//temp
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

	public override string GetObjectName()
	{
		return "Market";//TODO
	}

	public int GetBuyPrice(ResourceDefintion resource)
	{
		return (int)(buyPriceModifier * resource.price);
	}

	public int GetSellPrice(ResourceDefintion resource)
	{
		return (int)(sellPriceModifier * resource.price);
	}
}
