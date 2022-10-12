using UnityEngine;

public class Settlement : Building, IConsumer
{
	[SerializeField]
	private LandingPlatform platform;

	[SerializeField]
	private ResourceDefintion[] accepts;

	[SerializeField]
	private ConsumerWant[] consumerWants;


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


	public void SetWants(ConsumerWant[] consumerWants)
	{
		this.consumerWants = consumerWants;
	}

	public ConsumerWant[] GetWants()
	{
		return consumerWants;
	}

	public void SetAcceptedResources(ResourceDefintion[] resourceDefintions)
	{
		accepts = resourceDefintions;
	}
}
