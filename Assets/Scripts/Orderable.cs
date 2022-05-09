using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Orderable : MonoBehaviour
{
	public bool loopOrders = false; //need to replace rmeove with index to loop, can be used for automation
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
		currentOrder = order;
		currentOrder.BeginOrder();
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
