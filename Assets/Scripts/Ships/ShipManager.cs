using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class ShipManager : Singleton<ShipManager>
{
	public event Action<ShipObject> ShipSelectedEvent;
	public event Action<ShipObject> ShipDeSelectedEvent;

	private ShipObject shipSelected;  //object we have selected

	private void Awake()
	{
		_instance = this;
	}

	public void Start()
	{
		MouseInteractionTracker.instance.LeftClickNonInteractableEvent += DeSelectShipObject;
		MouseInteractionTracker.instance.LeftClickInteractableEvent += OnLeftClickInteractable;
		MouseInteractionTracker.instance.RightClickInteractableEvent += OnRightClickInteractable;
	}

	private void OnDestroy()
	{
		MouseInteractionTracker.instance.LeftClickNonInteractableEvent -= DeSelectShipObject;
		MouseInteractionTracker.instance.LeftClickInteractableEvent -= OnLeftClickInteractable;
		MouseInteractionTracker.instance.RightClickInteractableEvent -= OnRightClickInteractable;
	}

	private void OnRightClickInteractable(InteractableObject obj, Vector3 worldClickPosition)
	{
		if (shipSelected != null && obj.gameObject != shipSelected.gameObject)
		{
			if (!shipSelected.CanPushAction())
			{
				return;
			}

			//TODO, if only option is move , then move
			ShipActionSelectionInfo shipActionSelectionInfo = new ShipActionSelectionInfo(shipSelected, obj, worldClickPosition);
			ShipActionDefintion[] interactionActions = GetAvailableActions(shipActionSelectionInfo);

			ShipActionOptionsScreen.Create(shipActionSelectionInfo, interactionActions, OnShipActionSelected);

		}
	}

	private void OnShipActionSelected(ShipActionDefintion shipActionDefintion, ShipActionSelectionInfo shipActionSelectionInfo)
	{
		shipActionDefintion.OnOptionSelected(shipActionSelectionInfo, OnShipActionCreated);
	}

	private void OnShipActionCreated(ShipAction shipAction)
	{
		ShipObject shipObject = shipAction.shipObject;
		shipObject.PushAction(shipAction);
		UIManager.instance.CloseAllScreens();
		//Fire eVENT
	}

	private ShipActionDefintion[] GetAvailableActions(ShipActionSelectionInfo shipActionSelectionInfo)
	{
		List<ShipActionDefintion> orders = new List<ShipActionDefintion>();
		foreach (ShipActionDefintion availableOrder in shipActionSelectionInfo.ship.shipActions)
		{
			if (availableOrder.CanShowOption(shipActionSelectionInfo))
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

		this.shipSelected = shipObject;
		this.shipSelected.OnShipSelected();
		ShipSelectedEvent?.Invoke(shipObject);

	}

	private void DeSelectShipObject()
	{
		if (shipSelected != null)
		{
			shipSelected?.OnShipDeselected();
			shipSelected = null;
			ShipDeSelectedEvent?.Invoke(shipSelected);
		}
	}
}


