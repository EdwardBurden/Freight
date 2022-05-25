using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Order
{
	public event Action OrderCompleteEvent;
	public event Action OrderCancelledEvent;

	public Order(Orderable orderable)
	{
		this.orderable = orderable;
	}

	protected Orderable orderable;

	public abstract void BeginOrder();

	public virtual void UpdateOrder() { }

	public abstract void EndOrder();

	public abstract void CancelOrder();

	protected void OrderComplete()
	{
		OrderCompleteEvent?.Invoke();
	}

	protected void OrderCancelled()
	{
		OrderCancelledEvent?.Invoke();
	}
}
