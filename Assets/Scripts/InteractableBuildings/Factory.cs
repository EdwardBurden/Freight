using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : Building, IProducer, IConsumer
{
	public ResourceData[] GetAcceptedResources()
	{
		throw new System.NotImplementedException();
	}

	public ResourceData[] GetProducedResources()
	{
		throw new System.NotImplementedException();
	}
}
