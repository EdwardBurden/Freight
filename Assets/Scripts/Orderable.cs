using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Orderable : MonoBehaviour
{
	//	public bool loopOrders = false; //need to replace rmeove with index to loop, can be used for automation
	private bool active;
	private List<Order> orderQueue = new List<Order>();
	private Order currentOrder;

	private void Update()
	{
		if (active && currentOrder != null)
		{
			bool status = currentOrder.CheckOrderStatus(); //can be enum or struct later
			if (!status)
			{
				StopAllOrders();
				Debug.LogError("Cannot complete Order");
			}
		}
	}

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
		orderQueue.RemoveAt(0);

		currentOrder = order;
		currentOrder.BeginOrder();
	}

	private void StopAllOrders()
	{
		if (currentOrder != null)
		{
			currentOrder.EndOrder();
		}
		currentOrder = null;
		active = false;
		orderQueue.Clear();
	}

	private bool HasOrders()
	{
		return orderQueue.Count > 0;
	}
}
