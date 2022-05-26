using Behaviours;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Assume for now that 
public class PickUpOrder : InteractionOrder
{
	private MoveBehaviour movementBehaviour;
	private SingleCarryBehaviour carryBehaviour;

	private Freight pickUp => interactableObject.GetComponent<Freight>();
	public PickUpOrder(InteractableObject pickupObject, Orderable orderable, Vector3 postion) : base(orderable, pickupObject, postion)
	{
		carryBehaviour = orderable.GetComponent<SingleCarryBehaviour>();
		movementBehaviour = orderable.GetComponent<MoveBehaviour>();
		pickUp.RegisterCarrier(carryBehaviour);
	}

	public override void BeginOrder()
	{
		movementBehaviour.movementComplete += ReachedTarget;
		carryBehaviour.PickUpComplete += OrderComplete;


		movementBehaviour.StartMovement(pickUp.transform.position);
	}

	private void ReachedTarget()
	{
		carryBehaviour.StartPickup(pickUp);
	}

	public override void EndOrder()
	{
		movementBehaviour.StopMovement();
		carryBehaviour.StopPickUp();
		movementBehaviour.movementComplete -= ReachedTarget;
		carryBehaviour.PickUpComplete -= OrderComplete;
	}

	internal static bool CanCreate(ControllableObject controlledObject, InteractableObject targetInteractableObject)
	{
		Freight freight = targetInteractableObject.gameObject.GetComponent<Freight>();
		if (freight == null)
		{
			return false;
		}
		SingleCarryBehaviour carryBehaviour = controlledObject.gameObject.GetComponent<SingleCarryBehaviour>();
		if (carryBehaviour == null)
		{
			return false;
		}
		return freight.CanCarry() && carryBehaviour.HasCarrySpace();
	}

	public override void CancelOrder()
	{
			throw new NotImplementedException();
	}

}