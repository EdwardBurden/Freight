using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
	public ShipObject shipObject;  //object we have selected

	private void Awake()
	{
		MouseInteractionTracker.mouseLeftClickedInteractable += OnLeftClickInteractable;
		MouseInteractionTracker.mouseRightClickedInteractable += OnRightClickInteractable;
	}

	private void OnRightClickInteractable(InteractableObject obj, Vector3 worldClickPosition)
	{
		if (shipObject != null && obj.gameObject != shipObject)
		{
			InteractableObject targetInteractableObject = obj;
			//clickPosition = worldClickPosition;
			//OpenInteractionOptions(worldClickPosition);
		}
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
	}

	private void DeSelectShipObject()
	{
		shipObject.OnShipDeselected();
		shipObject = null;
	}
}
