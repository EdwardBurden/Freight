using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumer : Building
{
	private bool isLockedInInteraction;
	[SerializeField]
	private Transform deliverlocation;
	public List<ResourceDefintion> resourcesAccepted;

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
		Destroy(deliverlocation.transform.GetChild(0).gameObject); //maybe add wait/ add animation disappearing
	}

	private bool IsFreightInPostion()
	{
		return deliverlocation.GetComponentInChildren<Freight>() != null;
	}
}
