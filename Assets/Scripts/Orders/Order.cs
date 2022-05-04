using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Order
{
	public Order(Orderable orderable)
	{
		this.orderable = orderable;
	}

	protected Orderable orderable;

	//after creating an order
	//should be handed to queue and verified is valid/possible(item to pick up exists when given etc), ship has component needed to do task
	//order is added to queue and waits to tbe assigned current order
	//order is started 
	//order contains order information and passes whats needed to component to work
	//order accesses ship components to start tasks
	//order then waits for ship component to return with order status
	//status could be cancelled,complete dead ship etc
	//status is handled by queue

	//might not need
	//private  Action OrderStarted;
	//private  Action OrderStopped;

	//public Action OrderCancelled;
	//public Action OrderComplete;

	//public abstract bool IsOrderPossible(); return to later when maing system to plan order
	public abstract bool CanBeginOrder(); //abut to start order, check it can begin

	public abstract void BeginOrder();
	public abstract void EndOrder();

	public virtual void OrderComplete()
	{
		orderable.CurrentOrderComplete();
	}
}
