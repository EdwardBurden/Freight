using UnityEngine;

public class Farm : Building, IProducer
{
	[SerializeField]
	private LandingPlatform platform;

	[SerializeField]
	private ResourceDefintion[] produces;

	public LandingPlatform GetProducerLandingPlatform()
	{
		return platform;
	}

	public ResourceDefintion[] GetProducedResources()
	{
		return produces;
	}

	public bool ProducesResource(ResourceDefintion resourceData)
	{
		for (int i = 0; i < produces.Length; i++)
		{
			if (produces[i] == resourceData)
			{
				return true;
			}
		}
		return false;
	}
}
