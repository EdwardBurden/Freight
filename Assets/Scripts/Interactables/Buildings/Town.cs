using UnityEngine;

public class Town : Building, IConsumer
{
	[SerializeField]
	private float buyPriceModifier;

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

	public override string GetObjectName()
	{
		return "Town";
	}

	public int GetBuyPrice(ResourceDefintion resource)
	{
		return (int)(buyPriceModifier * resource.price);
	}
}
