using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProducer
{
	public ResourceData[] GetProducedResources();
	LandingPlatform GetLandingPlatform();
	bool ProducesResource(ResourceData resourceData);
}
