
//base ship class
using System;

public class ShipObject : InteractableObject
{

	public int speed; //TODO get from scriptable
	public int turnSpeed;


	public virtual void OnShipSelected() { }

	public virtual void OnShipDeselected() { }

	internal ShipActionList GetActionList()
	{
		throw new NotImplementedException();
	}
}
