using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct ShipActionSelectionInfo
{
	public ShipObject ship;
	public InteractableObject interactable;
	public Vector3 clickPosition;

	public ShipActionSelectionInfo(ShipObject ship, InteractableObject interactable, Vector3 clickPosition)
	{
		this.ship = ship;
		this.interactable = interactable;
		this.clickPosition = clickPosition;
	}
}
