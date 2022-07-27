using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class ShipSelectionManager : Singleton<ShipSelectionManager>
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
		RegisterEvents();
	}

	private void OnDestroy()
	{
		UnregisterEvents();
	}

	public void RegisterEvents()
	{
		MouseInteractionTracker.instance.LeftClickNonInteractableEvent += DeSelectShipObject;
		MouseInteractionTracker.instance.LeftClickInteractableEvent += OnLeftClickInteractable;
		MouseInteractionTracker.instance.RightClickInteractableEvent += OnRightClickInteractable;
	}

	public void UnregisterEvents()
	{
		MouseInteractionTracker.instance.LeftClickNonInteractableEvent -= DeSelectShipObject;
		MouseInteractionTracker.instance.LeftClickInteractableEvent -= OnLeftClickInteractable;
		MouseInteractionTracker.instance.RightClickInteractableEvent -= OnRightClickInteractable;
	}

	private void OnRightClickInteractable(InteractableObject obj, Vector3 worldClickPosition)
	{
		if (shipSelected != null && obj.gameObject != shipSelected.gameObject)
		{
			//TODO, if only option is move , then move
			RightClickContextActionInfo shipActionSelectionInfo = new RightClickContextActionInfo(shipSelected, obj, worldClickPosition);
			ShipActionDefintion[] interactionActions = GetAvailableActions(shipActionSelectionInfo);

			if (interactionActions.Length == 0)
			{
				//Show none available
				return;
			}

			if (interactionActions.Length == 1)
			{
				interactionActions[0].OnDefaultRightClickMenuOption(shipActionSelectionInfo, OnShipActionCreated);
				return;
			}
			ShipActionSelectScreen.Create(shipActionSelectionInfo, interactionActions, OnShipActionSelected);
		}
	}

	private void OnShipActionSelected(ShipActionDefintion shipActionDefintion, RightClickContextActionInfo shipActionSelectionInfo)
	{
		shipActionDefintion.OnOptionSelectedFromRightClickMenu(shipActionSelectionInfo, OnShipActionCreated);
	}

	private void OnShipActionCreated(ShipAction shipAction)
	{
		ShipObject shipObject = shipAction.shipObject;
		shipObject.SetAction(shipAction);
		UIManager.instance.CloseAllScreens();
		//Fire eVENT
	}

	private ShipActionDefintion[] GetAvailableActions(RightClickContextActionInfo shipActionSelectionInfo)
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


