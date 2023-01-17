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

	LandingPlatform IProducer.GetProducerLandingPlatform()
	{
		return platform;
	}

	ResourceDefintion[] IProducer.GetProducedResources()
	{
		return new ResourceDefintion[]
		{
			farmDefintion.resourceProduced
		};
	}

	bool IProducer.CanProducesResource(ResourceDefintion resourceData)
	{
		return farmDefintion.CanProducesResource(resourceData) && storage.HasStorage(resourceData);
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
		storage.AddStorage(farmDefintion.resourceProduced, farmDefintion.startingStorage);
	}

	private void OnSeasonEnd(SeasonType obj)
	{
		storage.RemoveStorage(farmDefintion.resourceProduced, farmDefintion.amountLostPerSeason);
		//more ot do later
	}

	private void OnSeasonStart(SeasonType obj)
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
	}
}
