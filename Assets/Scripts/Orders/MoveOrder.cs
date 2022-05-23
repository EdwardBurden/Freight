using Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOrder : InteractionOrder
{
	private MoveBehaviour movementBehaviour;
	private float speed;
	private float turnSpeed;
	public MoveOrder(InteractableObject pickupObject, Orderable orderable, Vector3 postion) : base(orderable, pickupObject, postion)
	{

	}

	public override void BeginOrder()
	{
		movementBehaviour = orderable.GetComponent<MoveBehaviour>();
		movementBehaviour.StartMovement(targetPosition);
		movementBehaviour.movementComplete += OrderComplete;
	}

	public override bool CheckOrderStatus()
	{
		return true;//check area is still accessible
	}

	public override void EndOrder()
	{
		movementBehaviour.StopMovement();
		movementBehaviour.movementComplete -= OrderComplete;
	}

	public static bool CanCreate(ControllableObject controllableObject, Vector3 postion)
	{
		return postion.FlatDistanceTo(controllableObject.transform.position) > 20;
	}
}
