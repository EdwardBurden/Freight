using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct ProductionOrder
{
	public ResourceDefintion resource;

	public ProductionOrder(ResourceDefintion resource)
	{
		this.resource = resource;
	}
}

public class Producer : Building
{
	//Temp
	public List<ResourceDefintion> resourcesProduced;
	//temp
	public List<ProductionOrder> productionQueue = new List<ProductionOrder>();
	private bool isLockedInInteraction;
	[SerializeField]
	private Freight frieghtPrefab;
	[SerializeField]
	private Transform spawnLocation;
	[SerializeField]
	private int timeBetweenSpawn;

	private void OnEnable()
	{
		StartCoroutine(WaitAndSpawn());
	}

	private void OnDisable()
	{
		StopCoroutine(WaitAndSpawn());
	}

	public Freight GetSpawnedFreight()
	{
		return spawnLocation.GetComponentInChildren<Freight>();
	}

	public Vector3 GetCollectionPoint()
	{
		return spawnLocation.position;
	}

	public void SetLock(bool lockInteraction)
	{
		isLockedInInteraction = lockInteraction;
	}

	public bool CanCollect()
	{
		return !isLockedInInteraction && HasFreightSpawned();
	}

	private bool HasFreightSpawned()
	{
		return spawnLocation.GetComponentInChildren<Freight>() != null;
	}
	private ProductionOrder GetNextProduction()
	{
		ProductionOrder productionOrder = productionQueue[0];
		return productionOrder;

	}
	private bool HasProductionQueue()
	{
		return productionQueue.Count > 0;

	}

	public void AddOrder(ResourceDefintion resource)
	{
		ProductionOrder productionOrder = new ProductionOrder(resource);
		productionQueue.Add(productionOrder);
		//StartCoroutine(WaitAndSpawn());
	}


	private IEnumerator WaitAndSpawn()
	{
		while (true)
		{
			if (HasProductionQueue() && !HasFreightSpawned())
			{
				ProductionOrder productionOrder = GetNextProduction();
				yield return new WaitForSeconds(productionOrder.resource.spawnTime);
				Instantiate(productionOrder.resource.FreightPrefab, spawnLocation);
				productionQueue.RemoveAt(0);
			}
			yield return null;
		}
	}
}
