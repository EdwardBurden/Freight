using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderListHUD : Menu
{
	protected override void RegisterEvents()
	{
		Orderable.orderGiven += Orderable_orderGiven;
		//MouseInteractionTracker.mouseLeftClickedInteractable += OnLeftClickInteractable;
		//	MouseInteractionTracker.mouseLeftClickedNothing += OnLeftClickNotInteractable;
		base.RegisterEvents();
	}

	private void Orderable_orderGiven(Orderable orderable, GameAction gameAction)
	{
		Show(orderable);
	}

	public void Show(Orderable orderable)
	{
		List<GameActionDefintion> gameActions = orderable.GetOrderListDefintions();
		Clear();
		Open();
		foreach (GameActionDefintion action in gameActions)
		{
			GameObject gameObject = action.CreateButton(menuTransfrom);
			Button button = gameObject.GetComponent<Button>();

		}
	}
}
