using Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Assume for now that 
public class PickUpOrder : InteractionOrder
{
	private MoveBehaviour movementBehaviour;
	private CarryBehaviour pickUpBehaviour;
	private Carryable pickUp => interactableObject.GetComponent<Carryable>();
	public PickUpOrder(InteractableObject pickupObject, Orderable orderable, Vector3 postion) : base(orderable, pickupObject, postion)
	{

	}

	public override void BeginOrder()
	{
		movementBehaviour = orderable.GetComponent<MoveBehaviour>();
		movementBehaviour.MovementComplete += ReachedTarget;

		pickUpBehaviour = orderable.GetComponent<CarryBehaviour>();
		pickUpBehaviour.PickUpComplete += OrderComplete;

		movementBehaviour.StartMovement(pickUp.transform.position);
	}

	private void ReachedTarget()
	{
		pickUpBehaviour.StartPickup(pickUp);
	}

	public override void EndOrder()
	{
		movementBehaviour.StopMovement();
		pickUpBehaviour.StopPickUp();
		movementBehaviour.MovementComplete -= ReachedTarget;
		pickUpBehaviour.PickUpComplete -= OrderComplete;
	}

	public override bool CheckOrderStatus()
	{
		if (pickUpBehaviour.HasCarryable())
		{
			return false;
		}

		if (!pickUpBehaviour.HasCarryable() && pickUp.isBeingCarried)
		{
			return false;
		}
		return true;
	}
}