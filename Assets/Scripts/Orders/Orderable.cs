using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Orderable : MonoBehaviour
{
	private bool loopOrders = true; //need to replace rmeove with index to loop, can be used for automation
	private bool active;
	private List<GameAction> orderQueue = new List<GameAction>();
	private int index = -1;
	private GameAction currentOrder
	{
		get
		{
			if (index != -1 && index < orderQueue.Count)
			{
				return orderQueue[index];
			}
			return null;
		}
	}

	private void Update()
	{
		if (active && currentOrder != null)
		{
			currentOrder.OrderUpdated();
		}
	}

	public void AddOrder(GameAction shipOrder)
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
			if (loopOrders)
			{
				index = -1;
				TryNextOrder();
			}
			else
			{
				StopAllOrders();
			}
		}

	}

	private void BeginNextOrder()
	{
		active = true;
		index++;
		//GameAction order = orderQueue[0];
		//	orderQueue.RemoveAt(0);

		//	currentOrder = order;
		currentOrder.OrderCompleteEvent += CurrentOrderComplete;
		currentOrder.OrderCancelledEvent += CurrentOrderCancelled;
		currentOrder.OrderStarted();
	}

	private void StopAllOrders()
	{
		if (currentOrder != null)
		{
			currentOrder.OrderFinished();
		}
		index = -1;
		//currentOrder = null;
		active = false;
		orderQueue.Clear();
	}

	private bool HasOrders()
	{
		return index < orderQueue.Count - 1;
	}

	private void CurrentOrderCancelled()
	{
		currentOrder.OrderCancelled();
		currentOrder.OrderCompleteEvent -= CurrentOrderComplete;
		currentOrder.OrderCompleteEvent -= CurrentOrderCancelled;
		//currentOrder = null;
		TryNextOrder();
	}

	private void CurrentOrderComplete()
	{
		currentOrder.OrderFinished();
		currentOrder.OrderCompleteEvent -= CurrentOrderComplete;
		currentOrder.OrderCompleteEvent -= CurrentOrderCancelled;
		//currentOrder = null;
		TryNextOrder();
	}
}
