using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
	public InteractionContextMenu interactionContextMenu; //placed on ui canvas for now
	private ControllableObject controlledObject;  //object we have selected

	private InteractableObject targetInteractableObject; //object right clicked
	private Vector3 clickPosition; //position of  right click

	private void Awake()
	{
		RegisterEvents();
	}


	private void RegisterEvents()
	{
		MouseInteractionTracker.mouseHoveredOnInteractable += OnHoverOnInteractable;
		MouseInteractionTracker.mouseHoveredOffInteractable += OnHoverOffInteractable;
		MouseInteractionTracker.mouseLeftClickedInteractable += OnLeftClickInteractable;
		MouseInteractionTracker.mouseLeftClickedNothing += OnLeftClickNotInteractable;
		MouseInteractionTracker.mouseRightClickedInteractable += OnRightClickInteractable;

		InteractionContextMenu.optionSelected += OnOptionSelected;
	}

	private void OnLeftClickNotInteractable()
	{

	}

	private void OnRightClickInteractable(InteractableObject obj, Vector3 worldClickPosition)
	{
		if (obj != controlledObject)
		{
			targetInteractableObject = obj;
			clickPosition = worldClickPosition;
			OpenInteractionOptions(worldClickPosition);

		}
	}

	private void OnLeftClickInteractable(InteractableObject obj)
	{
		if (obj is ControllableObject controllableObject && controllableObject != controlledObject)
		{
			SelectControllableObject(controllableObject);
		}
	}

	private void OnHoverOffInteractable(InteractableObject obj)
	{
		if (interactionContextMenu.IsMenuOpen())
		{
			return;
		}

		//targetInteractableObject = null;
		Debug.Log("Off " + obj.name);

	}

	private void OnHoverOnInteractable(InteractableObject obj)
	{
		if (interactionContextMenu.IsMenuOpen())
		{
			return;
		}
		//targetInteractableObject = obj;
		Debug.Log("On " + obj.name);
	}

	private void OpenInteractionOptions(Vector3 worldHitPosition)
	{
		//GO TO UI
		Debug.Log("OpenInteractionOptions");
		List<InteractionOrderDefintion> interactionOrderDefintions = controlledObject.GetAvailableInteractionOrders(targetInteractableObject, clickPosition);
		interactionContextMenu.DisplayOptions(worldHitPosition,interactionOrderDefintions);
	}

	private void OnOptionSelected(InteractionOrderDefintion interactionOrder)
	{
		InteractionOrder order = interactionOrder.CreateInteractionOrder(controlledObject, targetInteractableObject, clickPosition);
		controlledObject.orderable.AddOrder(order);
		controlledObject.orderable.StartOrders();
	}

	private void SelectControllableObject(ControllableObject controllableObject)
	{
		//controlledObject.ControlLost() //maybe event we'll see
		controlledObject = null;
		controlledObject = controllableObject;
		Debug.Log("New Controllable " + controllableObject.name);
	}
}
