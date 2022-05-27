using Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : FreightAction
{
	private MoveBehaviour movementBehaviour;
	public Vector3 position;

	public MoveAction(Vector3 position, Orderable orderable, ActionDefintion defintion) : base(orderable, defintion)
	{
		this.position = position;
	}

	public override void BeginOrder()
	{
		movementBehaviour = orderable.GetComponent<MoveBehaviour>();
		movementBehaviour.StartMovement(position);
		movementBehaviour.movementComplete += CompleteOrder;
	}

	public override void OrderFinished()
	{
		movementBehaviour.StopBehaviour();
		movementBehaviour.movementComplete -= CompleteOrder;
	}

	public override void OrderCancelled()
	{
		OrderFinished();
	}

	public override void UpdateOrder()
	{
		//
	}
}
