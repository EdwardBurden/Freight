using Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectAction : FreightAction
{
	public Producer producer;
	public Freight collectionObject;
	private MoveBehaviour movementBehaviour;
	private SingleCarryBehaviour carryBehaviour;


	public CollectAction(Freight collectionObject, Producer producer, Orderable orderable, ActionDefintion defintion) : base(orderable, defintion)
	{
		carryBehaviour = orderable.GetComponent<SingleCarryBehaviour>();
		movementBehaviour = orderable.GetComponent<MoveBehaviour>();
		this.collectionObject = collectionObject;
		this.producer = producer;
	}

	public override void BeginOrder()
	{
		if (!carryBehaviour.HasCarrySpace() || collectionObject == null)
		{
			CancelOrder();
			return;
		}

		movementBehaviour.movementComplete += ReachedTarget;
		carryBehaviour.PickUpComplete += CompleteOrder;

		movementBehaviour.StartMovement(collectionObject.transform.position);
	}

	private void ReachedTarget()
	{
		carryBehaviour.StartPickup(collectionObject);
	}

	public override void OrderFinished()
	{
		movementBehaviour.StopBehaviour();
		carryBehaviour.StopBehaviour();
		movementBehaviour.movementComplete -= ReachedTarget;
		carryBehaviour.PickUpComplete -= CompleteOrder;
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
