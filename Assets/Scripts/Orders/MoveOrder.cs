using Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOrder : InteractionOrder
{
	private MoveBehaviour movementBehaviour;

	public MoveOrder(InteractableObject pickupObject, Orderable orderable, Vector3 postion) : base(orderable, pickupObject, postion)
	{

	}

	public override void BeginOrder()
	{
		movementBehaviour = orderable.GetComponent<MoveBehaviour>();
		movementBehaviour.StartMovement(targetPosition);
		movementBehaviour.MovementComplete += OrderComplete;
	}

	public override bool CheckOrderStatus()
	{
		return true;//check area is still accessible
	}

	public override void EndOrder()
	{
		movementBehaviour.StopMovement();
		movementBehaviour.MovementComplete -= OrderComplete;
	}
}
