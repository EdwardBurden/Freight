using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectAction : GameAction
{
	private Producer producer;
	private MoveBehaviour movementBehaviour;
	private CollectBehaviour collectBehaviour;
	private WaitBehaviour waitBehaviour;
	private CarryStorage carryStorage;

	public CollectAction(Producer producer, Orderable orderable, GameActionDefintion defintion) : base(orderable, defintion)
	{
		collectBehaviour = orderable.GetComponent<CollectBehaviour>();
		movementBehaviour = orderable.GetComponent<MoveBehaviour>();
		waitBehaviour = orderable.GetComponent<WaitBehaviour>();
		carryStorage = orderable.GetComponent<CarryStorage>();
		this.producer = producer;
	}

	public override void OrderStarted()
	{
		if (!carryStorage.HasSpace())
		{
			CancelOrder();
			return;
		}

		MoveToProducer();

	}

	private void MoveToProducer()
	{
		movementBehaviour.movementComplete += ReachedProducer;
		collectBehaviour.PickUpComplete += CompleteOrder;

		movementBehaviour.StartMovement(producer.GetCollectionPoint());

	}

	private void ReachedProducer()
	{
		waitBehaviour.StartWait(() => producer.CanCollect(), ProducerIsFree);

	}

	private void ProducerIsFree()
	{
		producer.SetLock(true);
		collectBehaviour.StartPickup(producer.GetSpawnedFreight());

	}

	public override void OrderFinished()
	{
		producer.SetLock(false);
		movementBehaviour.StopBehaviour();
		collectBehaviour.StopBehaviour();
		movementBehaviour.movementComplete -= ReachedProducer;
		collectBehaviour.PickUpComplete -= CompleteOrder;
	}

	public override void OrderCancelled()
	{
		producer.SetLock(false);
		movementBehaviour.StopBehaviour();
		collectBehaviour.StopBehaviour();
		movementBehaviour.movementComplete -= ReachedProducer;
		collectBehaviour.PickUpComplete -= CompleteOrder;
	}

	public override void OrderUpdated()
	{
		//
	}
}
