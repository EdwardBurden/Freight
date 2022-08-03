using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProducer
{
	public ResourceDefintion[] GetProducedResources();
	LandingPlatform GetProducerLandingPlatform();
	bool ProducesResource(ResourceDefintion resourceData);
}
