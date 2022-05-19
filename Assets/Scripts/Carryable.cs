using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carryable : MonoBehaviour //could be a tag instead, can decide later
{
	public bool isBeingCarried; //simple can expand later

	internal void Carry()
	{
		isBeingCarried = true;
		GetComponent<Collider>().enabled = false;
	}

	internal void Drop()
	{
		isBeingCarried = false;
		GetComponent<Collider>().enabled = true;
	}
}
