using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class ShipSelectionManager : Singleton<ShipSelectionManager>
{
	public event Action<Ship> ShipSelectedEvent;
	public event Action<Ship> ShipDeSelectedEvent;

	public Ship shipSelected;


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
		InputManager.instance.LeftClickNonInteractableEvent += DeSelectShipObject;
		InputManager.instance.LeftClickInteractableEvent += OnLeftClickInteractable;
		InputManager.instance.RightClickInteractableEvent += OnRightClickInteractable;
	}

	public void UnregisterEvents()
	{
		InputManager.instance.LeftClickNonInteractableEvent -= DeSelectShipObject;
		InputManager.instance.LeftClickInteractableEvent -= OnLeftClickInteractable;
		InputManager.instance.RightClickInteractableEvent -= OnRightClickInteractable;
	}

	private void OnRightClickInteractable(InteractableObject obj, Vector3 worldClickPosition) //TODO move this out
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

	private void OnShipActionSelected(ShipActionDefintion shipActionDefintion, InteractableObject interactableObject, Vector3 pos)//TODO move this out
	{
		shipActionDefintion.ActionSelected(shipSelected, interactableObject, pos, SetShipsAction);
	}

	public void SetShipsAction(ShipAction shipAction) //TODO move this out
	{
		Ship shipObject = shipAction.shipObject;
		shipObject.SetAction(shipAction);
		UIManager.instance.CloseAllScreens();
		//Fire eVENT
	}

	public void PushShipsAction(ShipAction shipAction) //TODO move this out
	{
		Ship shipObject = shipAction.shipObject;
		shipObject.PushAction(shipAction);
		UIManager.instance.CloseAllScreens();
		//Fire eVENT
	}

	private void OnLeftClickInteractable(InteractableObject obj, Vector3 position)
	{
		Ship shipObject = obj.GetComponent<Ship>();
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

	private void SelectShipObject(Ship shipObject)
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


