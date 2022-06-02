
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverAction : GameAction
{
	public Consumer consumer;
	private MoveBehaviour movementBehaviour;
	private DeliverBehaviour deliverBehaviour;
	private WaitBehaviour waitBehaviour;
	private CarryStorage carryStorage;

	public DeliverAction(Consumer consumer, Orderable orderable, GameActionDefintion defintion) : base(orderable, defintion)
	{
		deliverBehaviour = orderable.GetComponent<DeliverBehaviour>();
		movementBehaviour = orderable.GetComponent<MoveBehaviour>();
		waitBehaviour = orderable.GetComponent<WaitBehaviour>();
		carryStorage = orderable.GetComponent<CarryStorage>();
		this.consumer = consumer;
	}

	public override void OrderStarted()
	{
		if (carryStorage.SelectFirstFreight() == null)
		{
			CancelOrder();
			return;
		}

		MoveToConsumer();
	}

	private void MoveToConsumer()
	{
		movementBehaviour.movementComplete += ReachedConsumer;
		deliverBehaviour.PlaceDownComplete += CompleteOrder;

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
		deliverBehaviour.StartPlaceDown(consumer.GetDevliveryPoint(), carryStorage.SelectFirstFreight());
	}

	public override void OrderFinished()
	{
		consumer.FreightDelivered(deliverBehaviour.target);
		consumer.SetLock(false);

		movementBehaviour.StopBehaviour();
		deliverBehaviour.StopBehaviour();
		movementBehaviour.movementComplete -= ReachedConsumer;
		deliverBehaviour.PlaceDownComplete -= CompleteOrder;
	}

	public override void OrderCancelled()
	{
		movementBehaviour.StopBehaviour();
		deliverBehaviour.StopBehaviour();
		movementBehaviour.movementComplete -= ReachedConsumer;
		deliverBehaviour.PlaceDownComplete -= CompleteOrder;
	}

	public override void OrderUpdated()
	{
		//
	}
}

