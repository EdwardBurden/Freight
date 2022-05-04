using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Orderable : MonoBehaviour
{
	private bool active;
	private List<Order> orderQueue = new List<Order>();
	private Order currentOrder;
	public void AddOrder(Order shipOrder)
	{
		orderQueue.Add(shipOrder);
	}

	public void StartOrders()
	{
		if (active)
		{
			return;
		}

		if (HasOrders())
		{
			active = true;
			BeginNextOrder();
		}
		else
		{
			StopAllOrders();
		}
	}

	public void CurrentOrderComplete()
	{
		currentOrder.EndOrder();
		if (HasOrders())
		{
			BeginNextOrder();
		}
		else
		{
			StopAllOrders();
		}
	}

	private void BeginNextOrder()
	{
		Order order = orderQueue[0];
		if (order.CanBeginOrder())
		{
			currentOrder = order;
			currentOrder.BeginOrder();
		}
		else
		{
			StopAllOrders();
		}
		orderQueue.RemoveAt(0);
	}

	private void StopAllOrders()
	{
		if (currentOrder != null)
		{
			currentOrder.EndOrder();
		}
		currentOrder = null;
		active = false;
	}

	private bool HasOrders()
	{
		return orderQueue.Count > 0;
	}
}
