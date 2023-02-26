using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumeRequestDefintion : ResourceRequestDefintion
{
	public ResourceDefintion resource;
	public int minAmount;
	public int maxAmount;

	public override ResourceRequest MakeRequest()
	{
		int amount = Random.Range(minAmount, maxAmount + 1);
		ConsumeRequest consumeRequest = new ConsumeRequest(resource, amount);
		return consumeRequest;
	}
}
