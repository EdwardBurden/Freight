using Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceDownOrder : InteractionOrder
{
	private MoveBehaviour movementBehaviour;
	private CarryBehaviour carryBehaviour;

	//private Carryable pickUp => interactableObject.GetComponent<Carryable>();
	private GameObject targetParent => interactableObject.gameObject;
	public PlaceDownOrder(InteractableObject targetPlaceDownObject, Orderable orderable, Vector3 postion) : base(orderable, targetPlaceDownObject, postion)
	{

	}

	public override void BeginOrder()
	{
		movementBehaviour = orderable.GetComponent<MoveBehaviour>();
		movementBehaviour.movementComplete += ReachedTarget;

		carryBehaviour = orderable.GetComponent<CarryBehaviour>();
		carryBehaviour.PlaeDownComplete += OrderComplete;

		movementBehaviour.StartMovement(targetPosition);
	}

	private void ReachedTarget()
	{
		carryBehaviour.StartPlaceDown(targetParent);
	}

	public override bool CheckOrderStatus()
	{
		return true;//can figure out later
	}

	public override void EndOrder()
	{
		movementBehaviour.StopMovement();
		carryBehaviour.StopPlaceDown();
		movementBehaviour.movementComplete -= ReachedTarget;
		carryBehaviour.PlaeDownComplete -= OrderComplete;
	}
}

