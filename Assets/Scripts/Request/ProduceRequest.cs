using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProduceRequest : ResourceRequest
{
	public ProduceRequest(ResourceDefintion resource, int amount) : base(resource, amount)
	{
	}

	public override string FormatRequest()
	{
		return $"Produce {amount} {resource.resourceName}'s";
	}
}
