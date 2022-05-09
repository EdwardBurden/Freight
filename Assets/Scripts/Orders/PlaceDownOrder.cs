using Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceDownOrder : InteractionOrder
{
	private MoveBehaviour movementBehaviour;
//	private PickUpBehaviour pickUpBehaviour;
	private GameObject pickUp => interactableObject.gameObject;
	public PlaceDownOrder(InteractableObject pickupObject, Orderable orderable) : base(orderable, pickupObject)
	{

	}

	public override void BeginOrder()
	{
		throw new System.NotImplementedException();
	}

	public override void EndOrder()
	{
		throw new System.NotImplementedException();
	}
}

