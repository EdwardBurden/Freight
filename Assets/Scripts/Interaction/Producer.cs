using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Producer : InteractableObject
{
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

	private IEnumerator WaitAndSpawn()
	{
		while (true)
		{
			yield return new WaitForSeconds(timeBetweenSpawn);
			if (!HasFreightSpawned())
			{
				Instantiate(frieghtPrefab, spawnLocation);
			}
		}

	}
}
