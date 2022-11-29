using System;
using UnityEngine;

public class Settlement : Building, IConsumer
{
	public event Action RequestsFulfilledEvent;

	[SerializeField] private LandingPlatform platform;

	private SettlementDefintion settlementDefintion => buildingDefintion as SettlementDefintion;

	private SettlementRequestData settlementRequests = null;

	private void OnEnable()
	{
		SeasonsController.YearEndedEvent += OnYearEnd;
		SeasonsController.YearStartedEvent += OnYearStart;
	}

	private void OnDisable()
	{
		SeasonsController.YearEndedEvent -= OnYearEnd;
		SeasonsController.YearStartedEvent -= OnYearStart;
	}

	ResourceDefintion[] IConsumer.GetAcceptedResources()
	{
		return settlementDefintion.resourcesAccepted;
	}

	bool IConsumer.AcceptsResource(ResourceDefintion resourceData)
	{
		return settlementDefintion.AcceptsResource(resourceData);
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

	private void OnYearStart(int year)
	{
		settlementRequests = new SettlementRequestData(settlementDefintion.resourcesAccepted, year);
		storage.ClearStorage();
		if (summaryUI != null)
		{
			summaryUI.Show();
		}
	}

	private void OnYearEnd(int obj)
	{
		CheckRequests();
		settlementRequests = null;
		storage.ClearStorage();
		if (summaryUI != null)
		{
			summaryUI.Show();
		}
	}

	public SettlementRequestData GetResourceRequirements()
	{
		return settlementRequests;
	}

	private void CheckRequests()
	{
		if (settlementRequests == null)
		{
			return;
		}

		if (settlementRequests.RequestsFulfilled(storage))
		{
			WalletController.instance.AddSettlementRequestFulfilledTransaction(settlementRequests.creditReward, this);
			RequestsFulfilledEvent?.Invoke();
		}
	}
}
