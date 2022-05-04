using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOrder : Order
{
	private MovementBehaviour movementBehaviour;
	private Vector3 target;

	public MoveOrder(Vector3 target, Orderable orderable) : base(orderable)
	{
		this.target = target;
	}
	public override void BeginOrder()
	{
		movementBehaviour = orderable.GetComponent<MovementBehaviour>();
		movementBehaviour.StartMovement(target);
		movementBehaviour.MovementComplete += OrderComplete;
	}

	public override bool CanBeginOrder()
	{
		return orderable.GetComponent<MovementBehaviour>() != null; //&& area is navigateable
	}

	public override void EndOrder()
	{
		movementBehaviour.MovementComplete -= OrderComplete;
		//do any cleanup
	}


}
