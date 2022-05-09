using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Assume for now that 
public class PickUpOrder : InteractionOrder
{
	private MoveBehaviour movementBehaviour;
	private PickUpBehaviour pickUpBehaviour;
	private GameObject pickUp => interactableObject.gameObject;
	public PickUpOrder(InteractableObject pickupObject, Orderable orderable) : base(orderable, pickupObject)
	{

	}

	public override void BeginOrder()
	{
		movementBehaviour = orderable.GetComponent<MoveBehaviour>();
		movementBehaviour.MovementComplete += ReachedTarget;

		pickUpBehaviour = orderable.GetComponent<PickUpBehaviour>();
		pickUpBehaviour.PickUpComplete += OrderComplete;

		movementBehaviour.StartMovement(pickUp.transform.position);
	}

	public override bool CanBeginOrder()
	{
		return orderable.GetComponent<MoveBehaviour>() != null && orderable.GetComponent<PickUpBehaviour>() != null; ;
	}

	private void ReachedTarget()
	{
		pickUpBehaviour.StartPickup(pickUp);
		//play pickupanimation
		//when aniamton reached lowest point, attach gameobject
		//wait for animation to complete
		//trigger ordercomplete
	}



	public override void EndOrder()
	{
		movementBehaviour.MovementComplete -= ReachedTarget;
		pickUpBehaviour.PickUpComplete -= OrderComplete;
	}
}