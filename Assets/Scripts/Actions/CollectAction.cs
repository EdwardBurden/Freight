using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectAction : GameAction
{
	private Producer producer;
	private MoveBehaviour movementBehaviour;
	private SingleCarryBehaviour carryBehaviour;
	private WaitBehaviour waitBehaviour;

	public CollectAction(Producer producer, Orderable orderable, GameActionDefintion defintion) : base(orderable, defintion)
	{
		carryBehaviour = orderable.GetComponent<SingleCarryBehaviour>();
		movementBehaviour = orderable.GetComponent<MoveBehaviour>();
		waitBehaviour = orderable.GetComponent<WaitBehaviour>();
		this.producer = producer;
	}

	public override void OrderStarted()
	{
		if (!carryBehaviour.HasCarrySpace())
		{
			CancelOrder();
			return;
		}

		MoveToProducer();

	}

	private void MoveToProducer()
	{
		movementBehaviour.movementComplete += ReachedProducer;
		carryBehaviour.PickUpComplete += CompleteOrder;

		movementBehaviour.StartMovement(producer.GetCollectionPoint());

	}

	private void ReachedProducer()
	{
		waitBehaviour.StartWait(() => producer.CanCollect(), ProducerIsFree);

	}

	private void ProducerIsFree()
	{
		producer.SetLock(true);
		carryBehaviour.StartPickup(producer.GetSpawnedFreight());

	}

	public override void OrderFinished()
	{
		producer.SetLock(false);
		movementBehaviour.StopBehaviour();
		carryBehaviour.StopBehaviour();
		movementBehaviour.movementComplete -= ReachedProducer;
		carryBehaviour.PickUpComplete -= CompleteOrder;
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
