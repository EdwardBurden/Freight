using UnityEngine;

public class Settlement : Building, IConsumer
{
	[SerializeField]
	private LandingPlatform platform;

	[SerializeField]
	private ResourceDefintion[] accepts;

	public ResourceDefintion[] GetAcceptedResources()
	{
		return accepts;
	}

	public bool AcceptsResource(ResourceDefintion resourceData)
	{
		for (int i = 0; i < accepts.Length; i++)
		{
			if (accepts[i] == resourceData)
			{
				return true;
			}
		}
		return false;
	}

	public LandingPlatform GetConsumerLandingPlatform()
	{
		return platform;
	}

	public void SetAcceptedResources(ResourceDefintion[] resourceDefintions)
	{
		accepts = resourceDefintions;
	}

	public void BuyResourceFromShip(ResourceDefintion resource, Ship ship)
	{
		WalletController.instance.AddSellTransaction(resource.sellPrice, this, ship, resource);
		//TODO Update stockpile by addign what is bought
	}
}
