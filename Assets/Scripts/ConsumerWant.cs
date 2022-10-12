using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ConsumerWant
{
	public ResourceDefintion resource;
	public int amount;

	public ConsumerWant(ResourceDefintion resource, int amount)
	{
		this.resource = resource;
		this.amount = amount;
	}
}
