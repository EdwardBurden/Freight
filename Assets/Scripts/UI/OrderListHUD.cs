using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderListHUD : Menu
{
	private Orderable orderable;

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

	public void Show(Orderable orderable)
	{
		this.orderable = orderable;
		List<GameActionDefintion> gameActions = orderable.GetOrderListDefintions();
		Clear();
		Open();
		foreach (GameActionDefintion action in gameActions)
		{
			GameObject gameObject = action.CreateButton(menuTransfrom);
			Button button = gameObject.GetComponent<Button>();

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
}
