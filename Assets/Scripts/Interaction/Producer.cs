using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Producer : InteractableObject
{
	public GameObject frieghtPrefab;
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
			if (CanSpawn())
			{
				Instantiate(frieghtPrefab, spawnLocation);
			}
		}

	}

	private bool CanSpawn()
	{
		return spawnLocation.GetComponentInChildren<Freight>() == null;

	}
}
