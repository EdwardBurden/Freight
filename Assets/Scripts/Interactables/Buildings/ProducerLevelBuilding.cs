using UnityEngine;

public class ProducerLevelBuilding : LevelBuilding, IProducer
{
	[SerializeField] private LandingPlatform platform;

	private ProducerLevelDefinition farmDefintion => buildingDefintion as ProducerLevelDefinition;

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
		foreach (var item in needs)
		{
			if (item is ProduceRequest produceRequest && produceRequest.resource == resourceData)
			{
				return true;
			}
		}
		return false;
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

	public override void Init(BuildingLevelDefintion buildingDefintion , Building building)
	{
		base.Init(buildingDefintion, building);
	//	storage.AddStorage(GetResourceProduced(), farmDefintion.startingStorage);
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
		return 0;
		//return storage.GetStorage(farmDefintion.resourceProduced);
	}

	public ResourceDefintion GetResourceProduced()
	{
		return needs[0].resource;
	}

	public int GetAmountProducedPerTurn()
	{
		return needs[0].amount;
	}
}
