using UnityEngine;

public class Settlement : Building, IConsumer
{
	[SerializeField] private LandingPlatform platform;

	private SettlementDefintion settlementDefintion => buildingDefintion as SettlementDefintion;

	public ResourceDefintion[] GetAcceptedResources()
	{
		return settlementDefintion.resourcesAccepted;
	}

	public bool AcceptsResource(ResourceDefintion resourceData)
	{
		return settlementDefintion.AcceptsResource(resourceData);
	}

	public LandingPlatform GetConsumerLandingPlatform()
	{
		return platform;
	}

	public void BuyResourceFromShip(ResourceDefintion resource, Ship ship)
	{
		WalletController.instance.AddSellTransaction(resource.sellPrice, this, ship, resource);
		storage.AddStorage(resource);
	}
}
