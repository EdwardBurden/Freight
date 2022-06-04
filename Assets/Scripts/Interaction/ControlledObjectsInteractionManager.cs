using System;
using System.Collections.Generic;
using UnityEngine;

public class ControlledObjectsInteractionManager : MonoBehaviour
{
	[SerializeField]
	private OrderListHUD orderListHUD;
	[SerializeField]
	private InteractionContextMenu interactionContextMenu; //placed on ui canvas for now

	public static ControllableObject controlledObject;  //object we have selected
	public static InteractableObject targetInteractableObject; //object right clicked
	public static Vector3 clickPosition; //position of  right click


	private void Awake()
	{
		MouseInteractionTracker.mouseLeftClickedInteractable += OnLeftClickInteractable;
		MouseInteractionTracker.mouseLeftClickedNothing += OnLeftClickNotInteractable;
		MouseInteractionTracker.mouseRightClickedNothing += OnRightClickNotInteractable;
		MouseInteractionTracker.mouseRightClickedInteractable += OnRightClickInteractable;
	}

	private void OnLeftClickNotInteractable()
	{
		CloseMenus();
	}

	private void OnRightClickNotInteractable()
	{
		CloseMenus();
	}

	private void OnRightClickInteractable(InteractableObject obj, Vector3 worldClickPosition)
	{
		if (controlledObject != null && obj != controlledObject)
		{
			targetInteractableObject = obj;
			clickPosition = worldClickPosition;
			OpenInteractionOptions(worldClickPosition);
		}
	}

	private void OnLeftClickInteractable(InteractableObject obj)
	{
		if (obj is ControllableObject controllableObject)
		{
			SelectControllableObject(controllableObject);
		}
		else
		{
			controlledObject = null;
			CloseMenus();
		}
	}

	private void OpenInteractionOptions(Vector3 worldHitPosition)
	{
		GameActionDefintion[] interactionActions = GetAvailableOrders();
		interactionContextMenu.DisplayOptions(worldHitPosition, interactionActions);
	}

	private void CloseMenus()
	{
		interactionContextMenu.Close();
		orderListHUD.Close();
	}

	private GameActionDefintion[] GetAvailableOrders()
	{
		List<GameActionDefintion> orders = new List<GameActionDefintion>();
		foreach (GameActionDefintion availableOrder in controlledObject.actionDefintions)
		{
			if (availableOrder.ActionAvailable(controlledObject, targetInteractableObject, clickPosition))
			{
				orders.Add(availableOrder);
			}
		}
		return orders.ToArray();
	}

	private void SelectControllableObject(ControllableObject controllableObject)
	{
		controlledObject = null;
		controlledObject = controllableObject;

		orderListHUD.Show(controlledObject.orderable);
	}
}
