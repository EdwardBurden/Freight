using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : Building, IProducer
{
	[SerializeField] private LandingPlatform platform;

	private MineDefintion mineDefintion => buildingDefintion as MineDefintion;

	private void OnEnable()
	{
		//SeasonsController.SeasonStartedEvent += OnSeasonStart;
		//SeasonsController.SeasonEndedEvent += OnSeasonEnd;
	}

	private void OnDisable()
	{
		//SeasonsController.SeasonStartedEvent -= OnSeasonStart;
		//SeasonsController.SeasonEndedEvent -= OnSeasonEnd;
	}

	LandingPlatform IProducer.GetProducerLandingPlatform()
	{
		return platform;
	}

	ResourceDefintion[] IProducer.GetProducedResources()
	{
		return new ResourceDefintion[]
		{
			mineDefintion.resourceProduced
		};
	}

	bool IProducer.CanProducesResource(ResourceDefintion resourceData)
	{
		return mineDefintion.CanProducesResource(resourceData) && storage.HasStorage(resourceData);
	}

	void IProducer.SellResourceToShip(ResourceDefintion resource, Ship ship)
	{
		WalletController.instance.AddBuyTransaction(resource.buyPrice, this, ship, resource);
		storage.RemoveStorage(resource);
	}

	Transform IProducer.GetProducerNavigationPostion()
	{
		return summaryUIAnchor;//temp
	}

	public override void Init(BuildingDefintion buildingDefintion)
	{
		base.Init(buildingDefintion);
		storage.AddStorage(mineDefintion.resourceProduced, mineDefintion.startingStorage);
	}

	private void OnTurnEnd(int turn)
	{
		//more ot do later
	}

	private void OnTurnStart(int turn)
	{
		storage.AddStorage(mineDefintion.resourceProduced, mineDefintion.amountProducedPerSeason);
	}

	/*public int GetCropsStoredAmount()
	{
		return storage.GetStorage(mineDefintion.resourceProduced);
	}

	public ResourceDefintion GetResourceProduced()
	{
		return farmDefintion.resourceProduced;
	}

	public int GetAmountProducedPerAutumn()
	{
		return farmDefintion.amountProducedPerAutumn;
	}

	public int GetAmountLostPerSeason()
	{
		return farmDefintion.amountLostPerSeason;
	}*/
}
