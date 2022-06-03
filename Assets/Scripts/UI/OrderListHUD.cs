using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderListHUD : Menu
{
	private Orderable orderable;
	[SerializeField]
	private Transform orderListTransform;

	protected override void RegisterEvents()
	{
		Orderable.orderGiven += Orderable_orderGiven;
		//MouseInteractionTracker.mouseLeftClickedInteractable += OnLeftClickInteractable;
		//	MouseInteractionTracker.mouseLeftClickedNothing += OnLeftClickNotInteractable;
		base.RegisterEvents();
	}

	private void Orderable_orderGiven(Orderable orderable, GameAction gameAction)
	{
		if (this.orderable == orderable)
		{
			Show(orderable);
		}
	}

	private void UpdateIndexTracker()
	{
		throw new NotImplementedException();
	}

	public void Show(Orderable orderable)
	{
		this.orderable = orderable;
		List<GameActionDefintion> gameActions = orderable.GetOrderListDefintions();
		Clear();
		Open();
		foreach (GameActionDefintion action in gameActions)
		{
			GameObject gameObject = action.CreateButton(orderListTransform);


		}
	}

	public void Pause()
	{
		orderable.PauseOrders();
	}
	public void Resume()
	{
		orderable.ResumeOrders();
	}

	protected override void Clear()
	{
		foreach (Transform child in orderListTransform.transform)
		{
			GameObject.Destroy(child.gameObject);
		}
	}

}

