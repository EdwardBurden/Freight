using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using Unity.VisualScripting;
using UnityEngine;

public class Well : Building, IProducer
{
	public event Action<int> WaterStoredChangedEvent;

	[SerializeField]
	private LandingPlatform platform;
	[SerializeField]
	private ResourceDefintion produce;
	[SerializeField]
	public int[] amountProducedPerSeason; //45,80,85,34

	[HideInInspector]
	public int waterStored; //temp

	private void OnEnable()
	{
		SeasonsController.SeasonStartedEvent += OnSeasonStart;
	}

	private void OnDisable()
	{
		SeasonsController.SeasonStartedEvent -= OnSeasonStart;
	}

	public override void OnBuildingPlaced()
	{
		waterStored = amountProducedPerSeason[(int)SeasonsController.instance.season];
		base.OnBuildingPlaced();
	}

	public bool CanProducesResource(ResourceDefintion resourceData)
	{
		if (produce == resourceData && waterStored > 0)
		{
			return true;
		}
		return false;
	}

	public ResourceDefintion[] GetProducedResources()
	{
		return new ResourceDefintion[] { produce };
	}

	public LandingPlatform GetProducerLandingPlatform()
	{
		return platform;
	}

	public void SellResourceToShip(ResourceDefintion resource, Ship ship)
	{
		WalletController.instance.AddBuyTransaction(resource.buyPrice, this, ship, resource);

		waterStored--;
		StorageChanged();
		WaterStoredChangedEvent?.Invoke(waterStored);
	}

	private  void OnSeasonStart(SeasonType seasonType)
	{
		waterStored += amountProducedPerSeason[(int)seasonType];
		StorageChanged();
		WaterStoredChangedEvent?.Invoke(waterStored);
	}
}
