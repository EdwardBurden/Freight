using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConsumerLevelBuilding : LevelBuilding, IConsumer
{
	[SerializeField] private LandingPlatform platform;
	private ConsumerLevelDefintion consumerLevelDefintion => buildingDefintion as ConsumerLevelDefintion;

	ResourceDefintion[] IConsumer.GetAcceptedResources()
	{
		return needs.GetResourcesFromRequests<ConsumeRequest>();// wants.GetResourcesFromRequests<ConsumeRequest>() ;
	}

	bool IConsumer.AcceptsResource(ResourceDefintion resourceData)
	{
		return needs.GetResourcesFromRequests<ConsumeRequest>().Contains(resourceData);
	}

	LandingPlatform IConsumer.GetConsumerLandingPlatform()
	{
		return platform;
	}

	void IConsumer.BuyResourceFromShip(ResourceDefintion resource, Ship ship)
	{
		WalletController.instance.AddSellTransaction(resource.sellPrice, this, ship, resource);
		storage.AddStorage(resource);
		needs.UpdateResourceRequests<ConsumeRequest>(resource, 1);
	}

	Transform IConsumer.GetConsumerNavigationPostion()
	{
		return summaryUIAnchor;//temp
	}
}
