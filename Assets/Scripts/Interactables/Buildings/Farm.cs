using UnityEngine;

public class Farm : Building, IProducer
{
	[SerializeField] private LandingPlatform platform;

	private FarmDefintion farmDefintion => buildingDefintion as FarmDefintion;

	private void OnEnable()
	{
		TurnsController.TurnStartedEvent += OnTurnStart;
		TurnsController.TurnEndedEvent += OnTurnEnd;
	}

	private void OnDisable()
	{
		TurnsController.TurnStartedEvent -= OnTurnStart;
		TurnsController.TurnEndedEvent -= OnTurnEnd;
	}

	LandingPlatform IProducer.GetProducerLandingPlatform()
	{
		return platform;
	}

	ResourceDefintion[] IProducer.GetProducedResources()
	{
		return new ResourceDefintion[]
		{
			GetResourceProduced()
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
		storage.AddStorage(GetResourceProduced(), farmDefintion.startingStorage);
	}

	private void OnTurnEnd(int turn)
	{
		storage.ClearStorage(); //TEMP
		//storage.RemoveStorage(farmDefintion.resourceProduced, farmDefintion.amountLostPerSeason);
		//more ot do later
	}

	private void OnTurnStart(int turn)
	{
		storage.AddStorage(GetResourceProduced(), GetAmountProducedPerTurn());
	}

	public int GetCropsStoredAmount()
	{
		return storage.GetStorage(farmDefintion.resourceProduced);
	}

	public ResourceDefintion GetResourceProduced()
	{
		return farmDefintion.resourceProduced;
	}

	public int GetAmountProducedPerTurn()
	{
		return farmDefintion.amountProducedPerTurn;
	}
}
