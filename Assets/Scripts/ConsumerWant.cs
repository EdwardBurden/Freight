using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ConsumerWant //created , remove form inspector soon
{
	public ResourceDefintion resource;
	public int amount;

	public int delivered; //

	public ConsumerWant(ResourceDefintion resource, int amount)
	{
		this.resource = resource;
		this.amount = amount;
		delivered = 0;
	}
}
