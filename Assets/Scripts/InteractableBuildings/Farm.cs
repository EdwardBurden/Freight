using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : Building, IProducer
{
	[SerializeField]
	private ResourceData[] produces; //temp


	public ResourceData[] GetProducedResources()
	{
		return produces;
	}
}
