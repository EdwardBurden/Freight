using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Orderable : MonoBehaviour
{
	//	public bool loopOrders = false; //need to replace rmeove with index to loop, can be used for automation
	private bool active;
	private List<FreightAction> orderQueue = new List<FreightAction>();
	private FreightAction currentOrder;

	private void Update()
	{
		if (active && currentOrder != null)
		{
			currentOrder.UpdateOrder(); 
		}
	}

	public void AddOrder(FreightAction shipOrder)
	{
		orderQueue.Add(shipOrder);
	}

	public void StartOrders()
	{
		if (active)
		{
			return;
		}
		TryNextOrder();
	}

	private void TryNextOrder()
	{
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
		active = true;
		FreightAction order = orderQueue[0];
		orderQueue.RemoveAt(0);

		currentOrder = order;
		currentOrder.OrderCompleteEvent += CurrentOrderComplete;
		currentOrder.OrderCancelledEvent += CurrentOrderCancelled;
		currentOrder.BeginOrder();
	}

	private void StopAllOrders()
	{
		if (currentOrder != null)
		{
			currentOrder.OrderFinished();
		}
		currentOrder = null;
		active = false;
		orderQueue.Clear();
	}

	private bool HasOrders()
	{
		return orderQueue.Count > 0;
	}

	private void CurrentOrderCancelled()
	{
		currentOrder.OrderCancelled();
		currentOrder.OrderCompleteEvent -= CurrentOrderComplete;
		currentOrder.OrderCompleteEvent -= CurrentOrderCancelled;
		currentOrder = null;
		TryNextOrder();
	}

	private void CurrentOrderComplete()
	{
		currentOrder.OrderFinished();
		currentOrder.OrderCompleteEvent -= CurrentOrderComplete;
		currentOrder.OrderCompleteEvent -= CurrentOrderCancelled;
		currentOrder = null;
		TryNextOrder();
	}
}
