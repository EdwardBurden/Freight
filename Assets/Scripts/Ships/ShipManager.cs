using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class ShipManager : Singleton<ShipManager>
{
	public static event Action<ShipObject> ShipSelectedEvent;
	public static event Action<ShipObject> ShipDeSelectedEvent;

	public ShipObject shipObject;  //object we have selected

	private void Awake()
	{
		MouseInteractionTracker.LeftClickNonInteractableEvent += DeSelectShipObject;
		MouseInteractionTracker.LeftClickInteractableEvent += OnLeftClickInteractable;
		MouseInteractionTracker.RightClickInteractableEvent += OnRightClickInteractable;

	}

	private void OnDestroy()
	{
		MouseInteractionTracker.LeftClickNonInteractableEvent -= DeSelectShipObject;
		MouseInteractionTracker.LeftClickInteractableEvent -= OnLeftClickInteractable;
		MouseInteractionTracker.RightClickInteractableEvent -= OnRightClickInteractable;
	}

	private void OnRightClickInteractable(InteractableObject obj, Vector3 worldClickPosition)
	{
		if (shipObject != null && obj.gameObject != shipObject.gameObject)
		{
			//TODO, if only option is move , then move

			InteractableObject targetInteractableObject = obj;
			ShipActionDefintion[] interactionActions = GetAvailableActions(targetInteractableObject, worldClickPosition);

			ShipActionOptionsScreen.ShipActionCreatedEvent += InteractionActionOptionsDialog_ShipActionCreatedEvent;
			ShipActionOptionsScreen.Create(shipObject, obj, interactionActions, worldClickPosition);
		}
	}

	private void InteractionActionOptionsDialog_ShipActionCreatedEvent(ShipAction shipAction)
	{
		ShipActionOptionsScreen.ShipActionCreatedEvent -= InteractionActionOptionsDialog_ShipActionCreatedEvent;
		shipObject.Push(shipAction);
	}

	private ShipActionDefintion[] GetAvailableActions(InteractableObject obj, Vector3 worldClickPosition)
	{
		List<ShipActionDefintion> orders = new List<ShipActionDefintion>();
		foreach (ShipActionDefintion availableOrder in shipObject.shipActions)
		{
			if (availableOrder.CanShowOption(shipObject, obj, worldClickPosition))
			{
				orders.Add(availableOrder);
			}
		}
		return orders.ToArray();
	}

	private void OnLeftClickInteractable(InteractableObject obj, Vector3 position)
	{
		ShipObject shipObject = obj.GetComponent<ShipObject>();
		if (shipObject != null)
		{
			DeSelectShipObject();
			SelectShipObject(shipObject);
		}
		else
		{
			DeSelectShipObject();
		}
	}

	private void SelectShipObject(ShipObject shipObject)
	{
		this.shipObject = shipObject;
		this.shipObject.OnShipSelected();
		ShipSelectedEvent?.Invoke(shipObject);
	}

	private void DeSelectShipObject()
	{
		shipObject?.OnShipDeselected();
		shipObject = null;
		ShipDeSelectedEvent?.Invoke(shipObject);
	}
}

