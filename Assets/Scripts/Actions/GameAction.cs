using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameAction
{
	protected Orderable orderable;
	public GameActionDefintion defintion;

	public event Action OrderCompleteEvent;
	public event Action OrderCancelledEvent;

	public GameAction(Orderable orderable, GameActionDefintion defintion)
	{
		this.orderable = orderable;
		this.defintion = defintion;
	}

	public abstract void OrderStarted();

	public abstract void OrderUpdated();

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
