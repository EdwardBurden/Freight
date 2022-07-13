
//base ship class
using ActionLists;
using System;
using System.Collections.Generic;

public class ShipObject : InteractableObject
{
	private ActionList actionList = new ActionList();
	private bool play = true;

	public int speed; //TODO get from scriptable
	public int turnSpeed;
	public ShipActionDefintion[] shipActions;

	public virtual void OnShipSelected() { }

	public virtual void OnShipDeselected() { }

	public void Push(ShipAction shipAction)
	{
		actionList.PushAction(shipAction);
	}

	private void Update()
	{
		actionList.Update();
	}
}
