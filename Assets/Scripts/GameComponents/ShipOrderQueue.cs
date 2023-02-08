using System;
using System.Collections.Generic;
using UnityEngine;

public class ShipOrderQueue : MonoBehaviour
{
	public event Action<ShipOrder> OrderPushedEvent;
	public event Action<ShipOrder> OrderStartedEvent;
	public event Action<ShipOrder, string> OrderFailedEvent;
	public event Action<ShipOrder> OrderFinishedEvent;

	public List<ShipOrder> allOrders = new List<ShipOrder>();
	[HideInInspector] public int maxOrders;

	private List<ShipOrder> queuedOrders = new List<ShipOrder>();
	private int queueIndex;


	public bool repeat { get; set; }
	public bool paused { get; set; }

	private void Awake()
	{
		repeat = false;
		paused = false;
		queueIndex = 0;
	}

	private void Update()
	{
		if (paused)
		{
			return;
		}

		if (IsEmpty())
		{
			if (repeat && allOrders.Count > 1)
			{
				ResetOrders();
			}
			return;
		}

		if (queueIndex < queuedOrders.Count)
		{
			ShipOrder shipOrder = queuedOrders[0];
			if (!shipOrder.started)
			{
				shipOrder.Start();
				OrderStartedEvent?.Invoke(shipOrder);
			}
			shipOrder.Update();
			if (shipOrder.isFailed)
			{
				OrderFailed(shipOrder, shipOrder.failMessage);
				return;
			}
			if (shipOrder.isFinished)
			{
				shipOrder.Finish();
				queuedOrders.RemoveAt(0);
				if (!repeat)
				{
					allOrders.Remove(shipOrder);
				}
				queueIndex++;
				OrderFinishedEvent?.Invoke(shipOrder);
			}
		}
		else
		{
			queueIndex = 0;
		}
	}

	public int GetOrderCount()
	{
		return allOrders.Count;
	}

	public void ClearOrders()
	{
		queueIndex = 0;
		allOrders.Clear();
		queuedOrders.Clear();
		//TODO add calls to each order with Cancelled method in order 
	}

	public void PushOrder(ShipOrder order)
	{
		order.Setup();
		allOrders.Add(order);
		queuedOrders.Add(order);
		OrderPushedEvent?.Invoke(order);
	}

	public void RemoveOrder(ShipOrder order)
	{
		allOrders.Remove(order);
		queuedOrders.Remove(order);
		//TODO add calls to each order with Cancelled method in order 
		//TODO maybe tell order to do somethign here, since its now not working
	}

	private void OrderFailed(ShipOrder shipOrder, string error)
	{
		paused = true;
		repeat = false;
		shipOrder.Setup();
		//TODO do something maybe
		OrderFailedEvent?.Invoke(shipOrder, error);
	}

	private bool IsEmpty()
	{
		return queuedOrders.Count == 0;
	}

	private void ResetOrders()
	{
		foreach (ShipOrder order in allOrders)
		{
			order.Setup();
			queuedOrders.Add(order);
		}
		queueIndex = 0;
	}
}