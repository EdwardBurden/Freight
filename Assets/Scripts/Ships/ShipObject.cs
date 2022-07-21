
//base ship class
using ActionLists;
using System;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class ShipObject : InteractableObject
{
	[SerializeField]
	private UnityEvent OnShipSelectedEvent;
	[SerializeField]
	private UnityEvent OnShipDeSelectedEvent;
	[SerializeField]
	private ShipActionList actionList;

	public int speed; //TODO get from scriptable, add init 
	public int turnSpeed;
	public ShipActionDefintion[] shipActions;
	public string shipName;

	public virtual void OnShipSelected()
	{
		OnShipSelectedEvent?.Invoke();
	}

	public virtual void OnShipDeselected()
	{
		OnShipDeSelectedEvent?.Invoke();
	}

	public void PushAction(ShipAction shipAction)
	{
		actionList.PushAction(shipAction);
	}

	internal bool CanPushAction()
	{
		return actionList.activelist.Count < actionList.maxActions;
	}

	public override string GetObjectName()
	{
		return shipName;
	}
}
