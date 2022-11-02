using System.Collections;
using System.Collections.Generic;
using UI;
using Unity.VisualScripting;
using UnityEngine;

public class Well : Building, IProducer
{
	[SerializeField]
	private LandingPlatform platform;
	[SerializeField]
	private ResourceDefintion produce;
	[SerializeField]
	private int[] amountProducedPerSeason; //45,80,85,34

	[HideInInspector]
	public int waterStored; //temp

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
		waterStored--; //temp
		WalletController.instance.AddBuyTransaction(resource.buyPrice, this, ship, resource);
		UpdateUI();
		//TODO Update stockpile by remoing one of what is sold
	}

	protected override void OnSeasonStart(SeasonType obj)
	{
		waterStored += amountProducedPerSeason[(int)obj];
		base.OnSeasonStart(obj);
	}

	protected override void UpdateUI()
	{
		WellBuildingDisplay_InWorld wellInWorldUI = inWorldUI.GetComponent<WellBuildingDisplay_InWorld>();
		wellInWorldUI.UpdateUI(GetObjectName(), produce, waterStored);
	}
}
