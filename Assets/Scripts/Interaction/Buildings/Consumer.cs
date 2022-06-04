using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumer : Building
{
	private bool isLockedInInteraction;
	[SerializeField]
	private int timeBetweenSpawn;
	[SerializeField]
	private Transform deliverlocation;

	private void OnEnable()
	{
		StartCoroutine(WaitAndEat());
	}

	private void OnDisable()
	{
		StopCoroutine(WaitAndEat());
	}

	public bool CanDeliver()
	{
		return !isLockedInInteraction && !IsFreightInPostion();
	}

	public Transform GetDevliveryPoint()
	{
		return deliverlocation;
	}
	public void SetLock(bool lockInteraction)
	{
		isLockedInInteraction = lockInteraction;
	}

	public void FreightDelivered(Freight freight)
	{
		freight.transform.parent = deliverlocation;
	}

	private bool IsFreightInPostion()
	{
		return deliverlocation.GetComponentInChildren<Freight>() != null;
	}

	private IEnumerator WaitAndEat()
	{
		while (true)
		{
			yield return new WaitForSeconds(timeBetweenSpawn);
			if (IsFreightInPostion())
			{
				Destroy(deliverlocation.transform.GetChild(0).gameObject);
			}
		}

	}
}
