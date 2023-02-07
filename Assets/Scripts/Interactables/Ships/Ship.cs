using System.Collections.Generic;
using UnityEngine;
using UI;
using System;

[RequireComponent(typeof(ShipOrderQueue))]
public class Ship : InteractableObject
{
	[SerializeField] protected Transform summaryUIAnchor;
	[SerializeField] public Outline outline;
	protected ShipOrderQueue shipOrderQueue;

	protected ShipDefintion shipDefintion;
	protected ShipSummary_InWorld summaryUI;

	private float speed;

	public virtual void Init(ShipDefintion shipDefintion)
	{
		this.shipDefintion = shipDefintion;
		shipOrderQueue = GetComponent<ShipOrderQueue>();
		shipOrderQueue.maxOrders = shipDefintion.maxOrders;

		if (shipDefintion.summaryUIPrefab != null && summaryUIAnchor != null)
		{
			summaryUI = Instantiate(shipDefintion.summaryUIPrefab, summaryUIAnchor);
			summaryUI.Init(this);
		}
		speed = shipDefintion.maxSpeed;
	}

	public override string GetInteractableObjectName()
	{
		return shipDefintion.name;
	}

	public virtual void PushAction(ShipOrder shipAction)
	{
		shipOrderQueue.PushOrder(shipAction);
	}

	public virtual void SetAction(ShipOrder shipAction)
	{
		shipOrderQueue.ClearOrders();
		shipOrderQueue.paused = false;
		shipOrderQueue.repeat = false;
		shipOrderQueue.PushOrder(shipAction);
	}

	public ShipActionDefintion[] GetAvailableActions(InteractableObject interactableObject, Vector3 position)
	{
		List<ShipActionDefintion> orders = new List<ShipActionDefintion>();
		foreach (ShipActionDefintion availableOrder in shipDefintion.shipActions)
		{
			if (availableOrder.CanBeCreated(this, interactableObject, position))
			{
				orders.Add(availableOrder);
			}
		}
		return orders.ToArray();
	}

	public override void OnHover()
	{
		base.OnHover();
		if (summaryUI != null)
		{
			summaryUI.OnShipHover();
		}
	}

	public override void OffHover()
	{
		base.OffHover();
		if (summaryUI != null)
		{
			summaryUI.OffShipHover();
		}
	}

	public float GetTurnSpeed()
	{
		return shipDefintion.turnSpeed;
	}

	public float GetSpeed()
	{
		return speed;
	}

	public ShipActionDefintion[] GetShipActions()
	{
		return shipDefintion.shipActions;
	}

	public void ModifySpeed(int speedIncrease)
	{
		speed += speedIncrease;
	}
}
