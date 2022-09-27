
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public abstract class Ship : InteractableObject
{
	[SerializeField]
	private UnityEvent OnShipSelectedEvent;
	[SerializeField]
	private UnityEvent OnShipDeSelectedEvent;

	//TODO get from scriptable, add init 
	public int speed;
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

	public override string GetObjectName()
	{
		return shipName;
	}
	public abstract void PushAction(ShipOrder shipAction);

	public abstract void SetAction(ShipOrder shipAction);

	public ShipActionDefintion[] GetAvailableActions(InteractableObject interactableObject, Vector3 position)
	{
		List<ShipActionDefintion> orders = new List<ShipActionDefintion>();
		foreach (ShipActionDefintion availableOrder in shipActions)
		{
			if (availableOrder.CanBeCreated(this, interactableObject, position))
			{
				orders.Add(availableOrder);
			}
		}
		return orders.ToArray();
	}
}
