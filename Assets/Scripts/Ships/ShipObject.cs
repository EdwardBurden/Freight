
//base ship class
using ActionLists;
using System;
using System.Collections.Generic;

public class ShipObject : InteractableObject
{

	public int speed; //TODO get from scriptable
	public int turnSpeed;
	public ShipActionDefintion[] shipActions;

	public virtual void OnShipSelected() { }

	public virtual void OnShipDeselected() { }

	internal ActionList GetActionList()
	{
		throw new NotImplementedException();//TODO
	}
}
