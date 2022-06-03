using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Orderable : MonoBehaviour
{
	public static event Action<Orderable, GameAction> orderGiven;


	public bool loopOrders = false; //need to replace rmeove with index to loop, can be used for automation
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

	public void ResumeOrders()//temp needs work
	{
		if (!active)
		{
			index--;
			TryNextOrder();
		}
	}
	public void PauseOrders() //temp needs work
	{
		if (active)
		{
			active = false;
			currentOrder.OrderCancelled();
			currentOrder.OrderCompleteEvent -= CurrentOrderComplete;
			currentOrder.OrderCancelledEvent -= CurrentOrderCancelled;

		}
	}


	public List<GameActionDefintion> GetOrderListDefintions()
	{
		List<GameActionDefintion> defintions = new List<GameActionDefintion>();
		foreach (GameAction gameAction in orderQueue)
		{
			defintions.Add(gameAction.defintion);
		}
		return defintions;
	}

	public void AddOrder(GameAction shipOrder)
	{
		orderQueue.Add(shipOrder);
		orderGiven?.Invoke(this, shipOrder);
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
			if (loopOrders && orderQueue.Count > 1)
			{
				StartOrders();
			}
		}

	}

	private void BeginNextOrder()
	{
		active = true;
		index++;

		currentOrder.OrderCompleteEvent += CurrentOrderComplete;
		currentOrder.OrderCancelledEvent += CurrentOrderCancelled;
		currentOrder.OrderStarted();
	}

	private void StopAllOrders()
	{
		index = -1;
		active = false;
	}

	private bool HasOrders()
	{
		return index < orderQueue.Count - 1;
	}

	private void FinishOrder(GameAction order)
	{
		order.OrderFinished();
		order.OrderCompleteEvent -= CurrentOrderComplete;
		order.OrderCancelledEvent -= CurrentOrderCancelled;
	}


	private void CurrentOrderCancelled() //return to later
	{
		currentOrder.OrderCancelled();
		currentOrder.OrderCompleteEvent -= CurrentOrderComplete;
		currentOrder.OrderCancelledEvent -= CurrentOrderCancelled;
		//currentOrder = null;
		StopAllOrders();
		orderQueue.Clear();
		Debug.LogError("cancelled");
		//tell user
		//ClearOrders();
		//TryNextOrder();
	}

	private void CurrentOrderComplete()
	{
		FinishOrder(currentOrder);
		TryNextOrder();
	}
}
