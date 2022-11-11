using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mine : Building, IProducer
{
	[SerializeField]
	private LandingPlatform platform;
	[SerializeField]
	private ResourceDefintion produce;
	[SerializeField]
	private int amountProducedPerSeason;

	[HideInInspector]
	public int storedOre; //temp

	private void OnEnable()
	{
		SeasonsController.SeasonStartedEvent += OnSeasonStart;
		SeasonsController.SeasonEndedEvent += OnSeasonEnd;
	}

	private void OnDisable()
	{
		SeasonsController.SeasonStartedEvent -= OnSeasonStart;
		SeasonsController.SeasonEndedEvent -= OnSeasonEnd;
	}

	public override void OnBuildingPlaced()
	{
		storedOre = amountProducedPerSeason;
		base.OnBuildingPlaced();
	}

	public bool CanProducesResource(ResourceDefintion resourceData)
	{
		if (produce == resourceData && storedOre > 0)
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
		storedOre--; //temp
		WalletController.instance.AddBuyTransaction(resource.buyPrice, this, ship, resource);
	}

	private  void OnSeasonEnd(SeasonType obj)
	{
		//TODO
		//Roll dice to destroy
		//if destoryed, tell player with notifiication
		//instead of replace building, replace prefab with destoryed one and change UI to hvae a repair button
	}

	private  void OnSeasonStart(SeasonType obj)
	{
		storedOre += amountProducedPerSeason;
	}
}
