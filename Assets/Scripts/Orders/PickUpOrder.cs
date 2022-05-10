using Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Assume for now that 
public class PickUpOrder : InteractionOrder
{
	private MoveBehaviour movementBehaviour;
	private CarryBehaviour pickUpBehaviour;
	private float speed;
	private float turnSpeed;
	private Carryable pickUp => interactableObject.GetComponent<Carryable>();
	public PickUpOrder(InteractableObject pickupObject, Orderable orderable, Vector3 postion, float speed, float turnSpeed) : base(orderable, pickupObject, postion)
	{
		this.speed = speed;
		this.turnSpeed = turnSpeed;
	}

	public override void BeginOrder()
	{
		movementBehaviour = orderable.GetComponent<MoveBehaviour>();
		movementBehaviour.movementComplete += ReachedTarget;

		pickUpBehaviour = orderable.GetComponent<CarryBehaviour>();
		pickUpBehaviour.PickUpComplete += OrderComplete;

		movementBehaviour.StartMovement(pickUp.transform.position ,speed , turnSpeed);
	}

	private void ReachedTarget()
	{
		pickUpBehaviour.StartPickup(pickUp, turnSpeed);
	}

	public override void EndOrder()
	{
		movementBehaviour.StopMovement();
		pickUpBehaviour.StopPickUp();
		movementBehaviour.movementComplete -= ReachedTarget;
		pickUpBehaviour.PickUpComplete -= OrderComplete;
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