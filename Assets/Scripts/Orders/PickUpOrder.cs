using Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Assume for now that 
public class PickUpOrder : InteractionOrder
{
	private MoveBehaviour movementBehaviour;
	private CarryBehaviour carryBehaviour;

	private Carryable pickUp => interactableObject.GetComponent<Carryable>();
	public PickUpOrder(InteractableObject pickupObject, Orderable orderable, Vector3 postion) : base(orderable, pickupObject, postion)
	{

	}

	public override void BeginOrder()
	{
		movementBehaviour = orderable.GetComponent<MoveBehaviour>();
		movementBehaviour.movementComplete += ReachedTarget;

		carryBehaviour = orderable.GetComponent<CarryBehaviour>();
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

	public override bool CheckOrderStatus()
	{

		//return here, get cary item currently has , check if its the the target, return okay
		//if not its already got a item and cant get another rturn false
		/*if (pickUpBehaviour.HasCarryable() && pickUp.isBeingCarried)
		{
			return false;
		}

		if (!pickUpBehaviour.HasCarryable() && pickUp.isBeingCarried)
		{
			return false;
		}*/
		return true;
	}
}