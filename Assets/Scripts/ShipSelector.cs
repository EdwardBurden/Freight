using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class ShipSelector : MonoBehaviour
{
	public static event Action<Ship> ShipSelectedEvent;
	public static event Action<Ship> ShipDeSelectedEvent;

	[HideInInspector]
	public Ship shipSelected;


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
		MouseInput.LeftClickNonInteractableEvent += DeSelectShipObject;
		MouseInput.LeftClickInteractableEvent += OnLeftClickInteractable;
		MouseInput.RightClickInteractableEvent += OnRightClickInteractable;
	}

	public void UnregisterEvents()
	{
		MouseInput.LeftClickNonInteractableEvent -= DeSelectShipObject;
		MouseInput.LeftClickInteractableEvent -= OnLeftClickInteractable;
		MouseInput.RightClickInteractableEvent -= OnRightClickInteractable;
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
		GameUIManager.instance.CloseAllScreens();
		//Fire eVENT
	}

	public void PushShipsAction(ShipAction shipAction) //TODO move this out
	{
		Ship shipObject = shipAction.shipObject;
		shipObject.PushAction(shipAction);
		GameUIManager.instance.CloseAllScreens();
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


