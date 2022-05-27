using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Producer : InteractableObject
{
	public Freight frieghtPrefab;
	public Transform spawnLocation;
	public int timeBetweenSpawn;

	private void OnEnable()
	{
		StartCoroutine(WaitAndSpawn());
	}

	private void OnDisable()
	{
		StopCoroutine(WaitAndSpawn());
	}

	private IEnumerator WaitAndSpawn()
	{
		while (true)
		{
			yield return new WaitForSeconds(timeBetweenSpawn);
			if (!HasFreigthSpawned())
			{
				 Instantiate(frieghtPrefab, spawnLocation);
			}
		}

	}

	public bool HasFreigthSpawned()
	{
		return spawnLocation.GetComponentInChildren<Freight>() != null;
	}

	public Freight GetSpawnedFreight()
	{
		return spawnLocation.GetComponentInChildren<Freight>();
	}
}
