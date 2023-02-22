using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ResourceRequest
{
	public ResourceDefintion resource;
	public int amount;

	public ResourceRequest(ResourceDefintion resource, int amount)
	{
		this.resource = resource;
		this.amount = amount;
	}
}
