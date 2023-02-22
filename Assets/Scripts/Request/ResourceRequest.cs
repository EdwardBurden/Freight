using System;
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

	public int amountFulfilled { get; private set; }

	public virtual bool RequestFulfilled()
	{
		return amountFulfilled >= amount;
	}

	public void UpdateRequest(int amount)
	{
		amountFulfilled += amount;
	}
}
