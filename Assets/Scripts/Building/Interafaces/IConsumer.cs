using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConsumer 
{
	public ResourceData[] GetAcceptedResources();
	LandingPlatform GetLandingPlatform();
}
