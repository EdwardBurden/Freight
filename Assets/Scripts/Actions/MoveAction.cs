
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : GameAction
{
	private MoveBehaviour movementBehaviour;
	public Vector3 position;

	public MoveAction(Vector3 position, Orderable orderable, GameActionDefintion defintion) : base(orderable, defintion)
	{
		this.position = position;
	}

	public override void OrderStarted()
	{
		movementBehaviour = orderable.GetComponent<MoveBehaviour>();

		movementBehaviour.movementComplete += CompleteOrder;
		movementBehaviour.StartMovement(position);
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

	public override void OrderUpdated()
	{
		//
	}
}
