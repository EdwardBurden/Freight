using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TODO

public class Factory : Building, IProducer, IConsumer
{
	[SerializeField]
	private LandingPlatform inputPlatform;

	[SerializeField]
	private LandingPlatform outputPlatform;


	[SerializeField]
	private ConversionDefintion conversion;


	public ResourceDefintion[] GetAcceptedResources()
	{
		List<ResourceDefintion> resourceDefintions = new List<ResourceDefintion>();
		foreach (ConversionInput input in conversion.inputs)
		{
			resourceDefintions.Add(input.resource);
		}
		return resourceDefintions.ToArray();
	}

	public ResourceDefintion[] GetProducedResources()
	{
		return conversion.outputs;
	}

	public LandingPlatform GetProducerLandingPlatform()
	{
		return outputPlatform;
	}

	public LandingPlatform GetConsumerLandingPlatform()
	{
		return inputPlatform;
	}

	public bool AcceptsResource(ResourceDefintion resourceData)
	{
		for (int i = 0; i < conversion.inputs.Length; i++)
		{
			if (conversion.inputs[i].resource == resourceData)
			{
				return true;
			}
		}
		return false;
	}

	public bool ProducesResource(ResourceDefintion resourceData)
	{
		for (int i = 0; i < conversion.outputs.Length; i++)
		{
			if (conversion.outputs[i] == resourceData)
			{
				return true;
			}
		}
		return false;
	}
}
