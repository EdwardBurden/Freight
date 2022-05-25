using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freight : InteractableObject //could be a tag instead, can decide later
{
	private bool isBeingCarried; //Is currently being carried in a ship
	private bool selectedForCarrying; //Ship register interest in picking up, unregisterd when placed own order begins

	private void Awake()
	{
		isBeingCarried = false;
		selectedForCarrying = false;
	}

	public bool CanCarry()
	{
		return !isBeingCarried && !selectedForCarrying;
	}

	public bool IsSelectedToCarry()
	{
		return selectedForCarrying;
	}

	public void SelectForCarrying(bool toggle)
	{
		selectedForCarrying = toggle;
	}

	public void Carry()
	{
		isBeingCarried = true;
		GetComponent<Collider>().enabled = false;
	}

	public void Drop()
	{
		isBeingCarried = false;
		selectedForCarrying = false;
		GetComponent<Collider>().enabled = true;
	}
}
