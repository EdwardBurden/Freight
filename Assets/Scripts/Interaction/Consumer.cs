using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumer : InteractableObject
{
	public int timeBetweenSpawn;
	public Transform deliverlocation;

	internal bool CanDeliver()
	{
		return !IsfreightInPostion();
	}
	private void OnEnable()
	{
		StartCoroutine(WaitAndEat());
	}

	private void OnDisable()
	{
		StopCoroutine(WaitAndEat());
	}

	private IEnumerator WaitAndEat()
	{
		while (true)
		{
			yield return new WaitForSeconds(timeBetweenSpawn);
			if (IsfreightInPostion())
			{
				Destroy(deliverlocation.transform.GetChild(0).gameObject);
			}
		}

	}

	public void FreightDelivered(Freight freight)
	{
		freight.transform.parent = deliverlocation;
	}

	public bool IsfreightInPostion()
	{
		return deliverlocation.GetComponentInChildren<Freight>() != null;
	}
}
