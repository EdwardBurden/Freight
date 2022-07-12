using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//base ship class
public class ShipObject : InteractableObject
{
	public int speed; //TODO get from scriptable
	public int turnSpeed;


	public virtual void OnShipSelected() { }

	public virtual void OnShipDeselected() { }
}
