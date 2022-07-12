using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stockpile
{
	private Dictionary<ResourceData, int> resourceAmounts = new Dictionary<ResourceData, int>();

	public Stockpile(ResourceData[] resources)
	{
		foreach (ResourceData resource in resources)
		{
			resourceAmounts.Add(resource, 0);
		}
	}

	public void AddStock(ResourceData resourceData)
	{
		resourceAmounts[resourceData] += 1;
	}

	public void RemoveStock(ResourceData resourceData)
	{
		resourceAmounts[resourceData] -= 1;
	}

	public int GetStock(ResourceData resourceData)
	{
		return resourceAmounts[resourceData];
	}
}
