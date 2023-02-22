using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlementRequest
{
	public int creditReward;
	public temp[] requests;


	public SettlementRequest(ResourceDefintion[] resourceDefintions, int settlementLevel)
	{
		creditReward = 100;


		int resourcesAmount = Mathf.Min(resourceDefintions.Length, (settlementLevel / 2) + 1);
		requests = new temp[resourcesAmount];

		for (int i = 0; i < resourcesAmount; i++)
		{
			int amount = Random.Range(1, settlementLevel * 2);
			temp settlementRequirement = new temp(amount, resourceDefintions[i]);
			creditReward += (resourceDefintions[i].sellPrice * 2 * settlementLevel);
			requests[i] = settlementRequirement;
		}
	}

	public bool RequestsFulfilled(Storage storage)
	{
		foreach (temp request in requests)
		{
			if (storage.GetStorage(request.resourceDefintion) < request.amountRequired)
			{
				return false;
			}
		}
		return true;
	}
}
