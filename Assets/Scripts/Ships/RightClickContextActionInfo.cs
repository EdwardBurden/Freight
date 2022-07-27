using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RightClickContextActionInfo
{
	public ShipObject ship;
	public InteractableObject interactable;
	public Vector3 clickPosition;

	public RightClickContextActionInfo(ShipObject ship, InteractableObject interactable, Vector3 clickPosition)
	{
		this.ship = ship;
		this.interactable = interactable;
		this.clickPosition = clickPosition;
	}
}
