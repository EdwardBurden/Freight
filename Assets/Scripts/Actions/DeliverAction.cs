
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverAction : GameAction
{
	public Consumer consumer;
	private MoveBehaviour movementBehaviour;
	private SingleCarryBehaviour carryBehaviour;
	private WaitBehaviour waitBehaviour;

	public DeliverAction(Consumer consumer, Orderable orderable, GameActionDefintion defintion) : base(orderable, defintion)
	{
		carryBehaviour = orderable.GetComponent<SingleCarryBehaviour>();
		movementBehaviour = orderable.GetComponent<MoveBehaviour>();
		waitBehaviour = orderable.GetComponent<WaitBehaviour>();
		this.consumer = consumer;
	}

	public override void OrderStarted()
	{
		if (carryBehaviour.HasCarrySpace())
		{
			CancelOrder();
			return;
		}

		MoveToConsumer();
	}

	private void MoveToConsumer()
	{
		movementBehaviour.movementComplete += ReachedConsumer;
		carryBehaviour.PlaeDownComplete += PlaceComplete;

		movementBehaviour.StartMovement(consumer.GetDevliveryPoint().position);
	}

	private void ReachedConsumer()
	{
		waitBehaviour.StartWait(() => consumer.CanDeliver(), ConsumerIsFree);
	//	carryBehaviour.StartPlaceDown(consumer.deliverlocation);
	}

	private void ConsumerIsFree()
	{
		consumer.SetLock(true);
		carryBehaviour.StartPlaceDown(consumer.GetDevliveryPoint());
	}

	private void PlaceComplete()
	{
		consumer.FreightDelivered(carryBehaviour.carryObject);
		carryBehaviour.carryObject = null;
		CompleteOrder();
	}

	public override void OrderFinished()
	{
		consumer.SetLock(false);
		movementBehaviour.StopBehaviour();
		carryBehaviour.StopBehaviour();
		movementBehaviour.movementComplete -= ReachedConsumer;
		carryBehaviour.PlaeDownComplete -= PlaceComplete;
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

