using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOrder : Order
{
	private MoveBehaviour movementBehaviour;
	private Vector3 target;

	public MoveOrder(Vector3 target, Orderable orderable) : base(orderable)
	{
		this.target = target;
	}
	public override void BeginOrder()
	{
		movementBehaviour = orderable.GetComponent<MoveBehaviour>();
		movementBehaviour.StartMovement(target);
		movementBehaviour.MovementComplete += OrderComplete;
	}

	public override bool CanBeginOrder()
	{
		return orderable.GetComponent<MoveBehaviour>() != null; //&& area is navigateable
	}

	public override void EndOrder()
	{
		movementBehaviour.MovementComplete -= OrderComplete;
		//do any cleanup
	}


}
