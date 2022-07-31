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
			ShipActionDefintion[] interactionActions = shipSelected.GetAvailableActions(obj, worldClickPosition);

			if (interactionActions.Length == 0)
			{
				//Show none available //TODO
				return;
			}

			if (interactionActions.Length == 1)
			{
				interactionActions[0].ActionSelected(shipSelected, obj, worldClickPosition, SetShipsAction);
				return;
			}
			ShipActionSelectScreen.Create(worldClickPosition, interactionActions, (def) => OnShipActionSelected(def, obj, worldClickPosition));
		}
	}

	private void OnShipActionSelected(ShipActionDefintion shipActionDefintion, InteractableObject interactableObject, Vector3 pos)
	{
		shipActionDefintion.ActionSelected(shipSelected, interactableObject, pos, SetShipsAction);
	}

	public void SetShipsAction(ShipAction shipAction)
	{
		ShipObject shipObject = shipAction.shipObject;
		shipObject.SetAction(shipAction);
		UIManager.instance.CloseAllScreens();
		//Fire eVENT
	}

	public void PushShipsAction(ShipAction shipAction)
	{
		ShipObject shipObject = shipAction.shipObject;
		shipObject.PushAction(shipAction);
		UIManager.instance.CloseAllScreens();
		//Fire eVENT
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


