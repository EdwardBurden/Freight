using System;
using System.Collections.Generic;
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

	private void OnHoverOffInteractable(InteractableObject obj)
	{
		if (interactionContextMenu.IsMenuOpen())
		{
			return;
		}
	}

	private void OnHoverOnInteractable(InteractableObject obj)
	{
		if (interactionContextMenu.IsMenuOpen())
		{
			return;
		}
	}

	private void OpenInteractionOptions(Vector3 worldHitPosition)
	{
		OrderType[] orderTypes = GetAvailableOrders();
		interactionContextMenu.DisplayOptions(worldHitPosition, orderTypes);
	}

	private void CloseInteractionOptions()
	{
		if (interactionContextMenu.IsMenuOpen())
		{
			interactionContextMenu.Close();
		}
	}

	private OrderType[] GetAvailableOrders()
	{
		List<OrderType> orders = new List<OrderType>();
		foreach (OrderType availableOrder in controlledObject.availableOrders)
		{
			foreach (OrderType targetOrder in targetInteractableObject.targetOrders)
			{
				if (availableOrder == targetOrder && CanCreate(availableOrder))
				{
					orders.Add(availableOrder);
				}
			}
		}
		return orders.ToArray();
	}

	private bool CanCreate(OrderType interactionOrder) //UGLY BUT WORKS 
	{
		switch (interactionOrder)
		{
			case OrderType.Move:
				return MoveOrder.CanCreate(controlledObject, clickPosition);
			case OrderType.PlaceDown:
				return PlaceDownOrder.CanCreate(controlledObject, targetInteractableObject , clickPosition);
			default:
				break;
		}
		return true;
	}


	private void OnOptionSelected(OrderType interactionOrder) //UGLY BUT WORKS 
	{
		Order order = null;
		switch (interactionOrder)
		{
			case OrderType.Move:
				order = new MoveOrder(targetInteractableObject, controlledObject.orderable, clickPosition);
				break;
			case OrderType.Pickup:
				order = new PickUpOrder(targetInteractableObject, controlledObject.orderable, clickPosition);
				break;
			case OrderType.PlaceDown:
				order = new PlaceDownOrder(targetInteractableObject, controlledObject.orderable, clickPosition);
				break;
			case OrderType.Wait:
				order = new WaitOrder(5, controlledObject.orderable);
				break;
			case OrderType.Attack:
				return;
			case OrderType.Die:
				return;
			default:
				break;
		}
		controlledObject.orderable.AddOrder(order);
		controlledObject.orderable.StartOrders();
	}

	private void SelectControllableObject(ControllableObject controllableObject)
	{
		controlledObject = null;
		controlledObject = controllableObject;
	}
}
