using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlementRequest
{
	public int amountRequired;
	public ResourceDefintion resourceDefintion;

	public SettlementRequest(int amountRequired, ResourceDefintion resourceDefintion)
	{
		this.amountRequired = amountRequired;
		this.resourceDefintion = resourceDefintion;
	}
}

public class SettlementRequests
{
	public int creditReward;
	public SettlementRequest[] requirements;


	public SettlementRequests(ResourceDefintion[] resourceDefintions, int year)
	{
		creditReward = 100;
		int resourcesAmount = Mathf.Min(resourceDefintions.Length, Random.Range(1, year));
		requirements = new SettlementRequest[resourcesAmount];

		for (int i = 0; i < resourcesAmount; i++)
		{
			int amount = Random.Range(1, year * 2);
			SettlementRequest settlementRequirement = new SettlementRequest(amount, resourceDefintions[i]);
			creditReward += (resourceDefintions[i].sellPrice * 5 * year);
			requirements[i] = settlementRequirement;
		}
	}

	public bool RequestsFulfilled(Storage storage)
	{
		foreach (SettlementRequest request in requirements)
		{
			if (storage.GetStorage(request.resourceDefintion) < request.amountRequired)
			{
				return false;
			}
		}
		return true;
	}
}
