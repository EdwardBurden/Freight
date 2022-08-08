using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConsumer
{
	public ResourceDefintion[] GetAcceptedResources();
	LandingPlatform GetConsumerLandingPlatform();
	bool AcceptsResource(ResourceDefintion resourceData);

	int GetBuyPrice(ResourceDefintion resource);
}
