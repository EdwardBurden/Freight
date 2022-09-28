using System;
using System.Collections.Generic;
using UnityEngine;

public class ShipOrderQueue : MonoBehaviour
{
	//TODO remove static from these, or add ShipOrderQueue and o logic where picked up
	public static event Action<ShipOrder> OrderPushedEvent;
	public static event Action<ShipOrder> OrderStartedEvent;
	public static event Action<ShipOrder, string> OrderFailedEvent;
	public static event Action<ShipOrder> OrderFinishedEvent;

	public List<ShipOrder> allOrders = new List<ShipOrder>();
	public int maxActions;

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
				//shipOrder.OrderFailedEvent += OnOrderFailed;
				shipOrder.Start();
				OrderStartedEvent?.Invoke(shipOrder);
			}
			shipOrder.Update();
			if (shipOrder.isFailed)
			{
				OnOrderFailed(shipOrder, shipOrder.failMessage);
				return;
			}
			if (shipOrder.isFinished)
			{
				shipOrder.Finish();
				//shipOrder.OrderFailedEvent -= OnOrderFailed;
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

	public int GetActionsCount()
	{
		return allOrders.Count;
	}

	public void ClearActions()
	{
		queueIndex = 0;
		allOrders.Clear();
		queuedOrders.Clear();
	}

	public void PushAction(ShipOrder action)
	{
		action.Setup();
		allOrders.Add(action);
		queuedOrders.Add(action);
		OrderPushedEvent?.Invoke(action);
	}

	public void RemoveAction(ShipOrder action)
	{
		allOrders.Remove(action);
		queuedOrders.Remove(action);
		//TODO maybe tell order to do somethign here, since its now not working
	}

	private void OnOrderFailed(ShipOrder shipOrder, string error)
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
		foreach (ShipOrder action in allOrders)
		{
			action.Setup();
			queuedOrders.Add(action);
		}
		queueIndex = 0;
	}
}