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

	public abstract bool CheckOrderStatus();

	public abstract void BeginOrder();
	public abstract void EndOrder();

	public virtual void OrderComplete()
	{
		orderable.CurrentOrderComplete();
	}
}
