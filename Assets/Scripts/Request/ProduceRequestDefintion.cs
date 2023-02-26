using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProduceRequestDefintion : ResourceRequestDefintion
{
	public ResourceDefintion resource;
	public int minAmount;
	public int maxAmount;

	public override ResourceRequest MakeRequest()
	{
		int amount = Random.Range(minAmount, maxAmount + 1);
		ProduceRequest produceRequest = new ProduceRequest(resource, amount);
		return produceRequest;
	}

	//TODO keep track f storage in here andhave building use to verify it can level  up 
}
