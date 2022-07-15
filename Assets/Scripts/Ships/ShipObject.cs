
//base ship class
using ActionLists;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

public class ShipObject : InteractableObject
{
	public UnityEvent OnShipSelectedEvent;
	public UnityEvent OnShipDeSelectedEvent;

	private ActionList actionList = new ActionList();
	private bool play = true;

	public int speed; //TODO get from scriptable
	public int turnSpeed;
	public ShipActionDefintion[] shipActions;

	public virtual void OnShipSelected() { OnShipSelectedEvent?.Invoke(); }

	public virtual void OnShipDeselected() { OnShipDeSelectedEvent?.Invoke(); }

	public void Push(ShipAction shipAction)
	{
		actionList.PushAction(shipAction);
	}

	private void Update()
	{
		actionList.Update();
	}
}
