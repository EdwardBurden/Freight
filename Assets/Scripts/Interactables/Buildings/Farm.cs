using Unity.VisualScripting;
using UnityEngine;

public class Farm : Building, IProducer
{
	[SerializeField]
	private LandingPlatform platform;

	[SerializeField]
	private ResourceDefintion produce;

	[SerializeField]
	private int amountProducedPerAutumn;
	[SerializeField]
	private int amountLostPerSeason;

	[HideInInspector]
	public int cropStored; //temp

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
		return new ResourceDefintion[] { produce };
	}

	public bool CanProducesResource(ResourceDefintion resourceData)
	{
		if (produce == resourceData && cropStored > 0)
		{
			return true;
		}
		return false;
	}

	public void SellResourceToShip(ResourceDefintion resource, Ship ship)
	{
		cropStored--;
		WalletController.instance.AddBuyTransaction(resource.buyPrice, this, ship, resource);
		//TODO Update stockpile by remoing one of what is sold
	}

	private  void OnSeasonEnd(SeasonType obj)
	{
		//TODO
		//Roll dice to destroy crop yield
		//if destoryed, tell player with notifiication
		cropStored -= amountLostPerSeason;
		if (cropStored < 0)
		{
			cropStored = 0;
		}
		//TODO tell ships in queue to stop
	}

	protected  void OnSeasonStart(SeasonType obj)
	{
		if (obj == SeasonType.Autumn)
		{
			cropStored += amountProducedPerAutumn;
		}
	}
}
