
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freight : InteractableObject //could be a tag instead, can decide later
{
	private bool isBeingCarried; //Is currently being carried in a ship

	protected override void Awake()
	{
		isBeingCarried = false;
	}

	public bool CanCarry()
	{
		return !isBeingCarried;
	}


	public void Carry()
	{
		isBeingCarried = true;
		GetComponent<Collider>().enabled = false;
	}

	public void Drop()
	{
		isBeingCarried = false;
		GetComponent<Collider>().enabled = true;
	}
}
