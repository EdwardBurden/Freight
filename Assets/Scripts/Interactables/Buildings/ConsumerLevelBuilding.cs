using System;
using System.Collections.Generic;
using UnityEngine;

public class ConsumerLevelBuilding : LevelBuilding, IConsumer
{
	public event Action RequestsFulfilledEvent;

	[SerializeField] private LandingPlatform platform;

	private ConsumerLevelDefintion consumerLevelDefintion => buildingDefintion as ConsumerLevelDefintion;

	public int level = 1;

	private void OnEnable()
	{
		//SeasonsController.YearEndedEvent += OnYearEnd;
		//SeasonsController.YearStartedEvent += OnYearStart;
	}

	private void OnDisable()
	{
		//SeasonsController.YearEndedEvent -= OnYearEnd;
		//SeasonsController.YearStartedEvent -= OnYearStart;
	}

	ResourceDefintion[] IConsumer.GetAcceptedResources()
	{
		List<ResourceDefintion> resourceDefintions = new List<ResourceDefintion>();
		foreach (var item in needs)
		{
			resourceDefintions.Add(item.resource);
		}
		return resourceDefintions.ToArray();
	}

	bool IConsumer.AcceptsResource(ResourceDefintion resourceData)
	{
		foreach (var item in needs)
		{
			if (item is ConsumeRequest consumeRequest && consumeRequest.resource == resourceData)
			{
				return true;
			}
		}
		return false;
	}

	LandingPlatform IConsumer.GetConsumerLandingPlatform()
	{
		return platform;
	}

	void IConsumer.BuyResourceFromShip(ResourceDefintion resource, Ship ship)
	{
		WalletController.instance.AddSellTransaction(resource.sellPrice, this, ship, resource);
		storage.AddStorage(resource);
	}

	Transform IConsumer.GetConsumerNavigationPostion()
	{
		return summaryUIAnchor;//temp
	}

	private void OnYearStart(int year)
	{
		//settlementRequests = new SettlementRequest(settlementDefintion.resourcesAccepted, level);
		storage.ClearStorage();
		if (summaryUI != null)
		{
			summaryUI.Show();
		}
	}

	private void OnYearEnd(int obj)
	{
		CheckRequests();
		//settlementRequests = null;
		storage.ClearStorage();
		if (summaryUI != null)
		{
			summaryUI.Show();
		}
	}

	private void CheckRequests()
	{
		/*if (settlementRequests == null)
		{
			return;
		}

		if (settlementRequests.RequestsFulfilled(storage))
		{
			WalletController.instance.AddSettlementRequestFulfilledTransaction(settlementRequests.creditReward, this);
			RequestsFulfilledEvent?.Invoke();
			LevelUpSettlement();
		}
		else
		{
			LevelDownSettlement();
		}*/
	}

	private void LevelUpSettlement()
	{
		level++;
	}

	private void LevelDownSettlement()
	{
		if (level > 1)
		{
			level--;
		}
	}
}
