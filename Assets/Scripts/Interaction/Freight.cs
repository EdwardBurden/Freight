using Behaviours;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freight : InteractableObject //could be a tag instead, can decide later
{
	private bool isBeingCarried; //Is currently being carried in a ship
	private SingleCarryBehaviour registeredCarrier; //Ship register interest in picking up, unregisterd when

	protected override void Awake()
	{
		isBeingCarried = false;
		registeredCarrier = null;
	}

	public bool CanCarry()
	{
		return !isBeingCarried && registeredCarrier == null;
	}

	public bool HasCarrier()
	{
		return registeredCarrier != null;
	}

	public void RegisterCarrier(SingleCarryBehaviour carryBehaviour)
	{
		registeredCarrier = carryBehaviour;
	}

	public void Carry()
	{
		isBeingCarried = true;
		GetComponent<Collider>().enabled = false;
	}

	public void Drop()
	{
		isBeingCarried = false;
		registeredCarrier = null;
		GetComponent<Collider>().enabled = true;
	}
}
