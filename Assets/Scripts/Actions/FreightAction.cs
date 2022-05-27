using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FreightAction
{
	protected Orderable orderable;
	public ActionDefintion defintion;

	public event Action OrderCompleteEvent;
	public event Action OrderCancelledEvent;

	public FreightAction(Orderable orderable, ActionDefintion defintion)
	{
		this.orderable = orderable;
		this.defintion = defintion;
	}

	public abstract void BeginOrder();

	public abstract void UpdateOrder();

	public abstract void OrderFinished();

	public abstract void OrderCancelled();

	protected void CompleteOrder()
	{
		OrderCompleteEvent?.Invoke();
	}

	protected void CancelOrder()
	{
		OrderCancelledEvent?.Invoke();
	}
}
