using System;
using UI;
using UnityEngine;

public class ShipSelectionController : Singleton<ShipSelectionController>, IGameController
{
	public static event Action<Ship> ShipSelectedEvent;
	public static event Action<Ship> ShipDeSelectedEvent;

	private Ship shipSelected;

	private void Awake()
	{
		_instance = this;
	}

	public void Init()
	{
		DeSelectShipObject();
	}

	public void OnEnable()
	{
		MouseInput.LeftClickNonInteractableEvent += DeSelectShipObject;
		MouseInput.LeftClickInteractableEvent += OnLeftClickInteractable;
		MouseInput.RightClickInteractableEvent += OnRightClickInteractable;
	}

	public void OnDisable()
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

	public void SetShipsAction(ShipOrder shipAction) //TODO move this out
	{
		Ship shipObject = shipAction.shipObject;
		shipObject.SetAction(shipAction);
		GameUIController.instance.CloseAllScreens();
		//Fire eVENT
	}

	public void PushShipsAction(ShipOrder shipAction) //TODO move this out
	{
		Ship shipObject = shipAction.shipObject;
		shipObject.PushAction(shipAction);
		GameUIController.instance.CloseAllScreens();
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
		ShipSelectedEvent?.Invoke(shipObject);

	}

	private void DeSelectShipObject()
	{
		if (shipSelected != null)
		{
			shipSelected = null;
			ShipDeSelectedEvent?.Invoke(shipSelected);
		}
	}
}


