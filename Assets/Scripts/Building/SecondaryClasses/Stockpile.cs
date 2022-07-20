using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO
public class Stockpile
{
	private Dictionary<ResourceDefintion, int> resourceAmounts = new Dictionary<ResourceDefintion, int>();

	public Stockpile(ResourceDefintion[] resources)
	{
		foreach (ResourceDefintion resource in resources)
		{
			resourceAmounts.Add(resource, 0);
		}
	}

	public void AddStock(ResourceDefintion resourceData)
	{
		resourceAmounts[resourceData] += 1;
	}

	public void RemoveStock(ResourceDefintion resourceData)
	{
		resourceAmounts[resourceData] -= 1;
	}

	public int GetStock(ResourceDefintion resourceData)
	{
		return resourceAmounts[resourceData];
	}
}
