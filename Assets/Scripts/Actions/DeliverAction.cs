using Behaviours;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverAction : FreightAction
{
	public Consumer consumer;
	private MoveBehaviour movementBehaviour;
	private SingleCarryBehaviour carryBehaviour;

	public DeliverAction(Consumer consumer, Orderable orderable, ActionDefintion defintion) : base(orderable, defintion)
	{
		carryBehaviour = orderable.GetComponent<SingleCarryBehaviour>();
		movementBehaviour = orderable.GetComponent<MoveBehaviour>();
		this.consumer = consumer;
	}

	public override void BeginOrder()
	{
		if (carryBehaviour.HasCarrySpace())
		{
			CancelOrder();
			return;
		}

		movementBehaviour.movementComplete += ReachedTarget;
		carryBehaviour.PlaeDownComplete += PlaceComplete;

		movementBehaviour.StartMovement(consumer.deliverlocation.position);
	}

	private void ReachedTarget()
	{
		carryBehaviour.StartPlaceDown(consumer.deliverlocation);
	}

	private void PlaceComplete()
	{
		consumer.FreightDelivered(carryBehaviour.carryObject);
		carryBehaviour.carryObject = null;
		CompleteOrder();
	}

	public override void OrderFinished()
	{
		movementBehaviour.StopBehaviour();
		carryBehaviour.StopBehaviour();
		movementBehaviour.movementComplete -= ReachedTarget;
		carryBehaviour.PlaeDownComplete -= PlaceComplete;
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

