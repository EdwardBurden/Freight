using System;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Farm : Building, IProducer
{
	[SerializeField] private LandingPlatform platform;

	private FarmDefintion farmDefintion => buildingDefintion as FarmDefintion;

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

	public LandingPlatform GetProducerLandingPlatform()
	{
		return platform;
	}

	public ResourceDefintion[] GetProducedResources()
	{
		return new ResourceDefintion[]
		{
			farmDefintion.resourceProduced
		};
	}

	public bool CanProducesResource(ResourceDefintion resourceData)
	{
		return farmDefintion.CanProducesResource(resourceData) && HasStorageOfProduce(resourceData);
	}

	private bool HasStorageOfProduce(ResourceDefintion resourceData)
	{
		return storage.HasStorage(resourceData);
	}

	public void SellResourceToShip(ResourceDefintion resource, Ship ship)
	{
		WalletController.instance.AddBuyTransaction(resource.buyPrice, this, ship, resource);
		storage.RemoveStorage(resource);
	}

	private void OnSeasonEnd(SeasonType obj)
	{
		storage.RemoveStorage(farmDefintion.resourceProduced, farmDefintion.amountLostPerSeason);
		//more ot do later
	}

	protected void OnSeasonStart(SeasonType obj)
	{
		if (obj == SeasonType.Autumn)
		{
			storage.AddStorage(farmDefintion.resourceProduced, farmDefintion.amountProducedPerAutumn);
		}
	}

	public int GetCropsStoredAmount()
	{
		return storage.GetStorage(farmDefintion.resourceProduced);
	}

	internal ResourceDefintion GetResourceProduced()
	{
		return farmDefintion.resourceProduced;
	}
}
