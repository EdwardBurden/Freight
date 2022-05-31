using System;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
	public InteractionContextMenu interactionContextMenu; //placed on ui canvas for now
	public ControllableObject controlledObject;  //object we have selected

	private InteractableObject targetInteractableObject; //object right clicked
	private Vector3 clickPosition; //position of  right click

	private void Awake()
	{
		RegisterEvents();
	}

	private void RegisterEvents()
	{
		//	MouseInteractionTracker.mouseHoveredOnInteractable += OnHoverOnInteractable;
		//	MouseInteractionTracker.mouseHoveredOffInteractable += OnHoverOffInteractable;
		MouseInteractionTracker.mouseLeftClickedInteractable += OnLeftClickInteractable;
		MouseInteractionTracker.mouseLeftClickedNothing += OnLeftClickNotInteractable;
		MouseInteractionTracker.mouseRightClickedNothing += OnRightClickNotInteractable;
		MouseInteractionTracker.mouseRightClickedInteractable += OnRightClickInteractable;

		InteractionContextMenu.optionSelected += OnOptionSelected;
	}

	private void OnLeftClickNotInteractable()
	{
		CloseInteractionOptions();
	}

	private void OnRightClickNotInteractable()
	{
		CloseInteractionOptions();
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
		}
		CloseInteractionOptions();
	}

	private void OpenInteractionOptions(Vector3 worldHitPosition)
	{
		GameActionDefintion[] interactionActions = GetAvailableOrders();
		interactionContextMenu.DisplayOptions(worldHitPosition, interactionActions);
	}

	private void CloseInteractionOptions()
	{
		if (interactionContextMenu.IsMenuOpen())
		{
			interactionContextMenu.Close();
		}
	}

	private GameActionDefintion[] GetAvailableOrders()
	{
		List<GameActionDefintion> orders = new List<GameActionDefintion>();
		foreach (GameActionDefintion availableOrder in controlledObject.actionDefintions)
		{
			if (availableOrder.ActionAvailable(controlledObject, targetInteractableObject , clickPosition))
			{
				orders.Add(availableOrder);
			}
		}
		return orders.ToArray();
	}

	private void OnOptionSelected(GameActionDefintion interactionOrder) //UGLY BUT WORKS 
	{
		interactionOrder.OnActionSelected(controlledObject, targetInteractableObject, clickPosition);
	}

	private void SelectControllableObject(ControllableObject controllableObject)
	{
		controlledObject = null;
		controlledObject = controllableObject;

	}
}
