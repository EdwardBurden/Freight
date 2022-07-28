using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class MoveActionPreview : ActionPreview //or created in the scen froma prefab and killed when doen, can decide later
{
	private Action<Vector3> callback;

	protected override void OnRightClickInteractable(InteractableObject interactableObject, Vector3 position)
	{
		Close();
	}

	protected override void OnLeftClickInteractable(InteractableObject interactableObject, Vector3 position)
	{
		callback.Invoke(position);
		Close();
	}

	public void Open(ShipObject shipObject, Action<Vector3> callback)
	{
		this.callback = callback;
		this.shipObject = shipObject;
		Open();
	}

}
