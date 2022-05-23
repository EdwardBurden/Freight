using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
[RequireComponent(typeof(Orderable))]
public class ControllableObject : InteractableObject
{
	public OrderType[] availableOrders; //orders thsi object can be given
	public Orderable orderable => GetComponent<Orderable>();


}
