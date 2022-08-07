using ActionLists;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverAction : ShipAction
{
	private CarryObject carryObject;
	private IConsumer consumer;
	private ResourceDefintion resourceData;

	public DeliverAction(ShipActionDefintion shipActionDefintion, Ship shipObject, IConsumer target, ResourceDefintion resourceTarget) : base(shipActionDefintion, shipObject)
	{
		this.carryObject = shipObject.GetComponent<CarryObject>();
		this.consumer = target;
		this.resourceData = resourceTarget;
		Reset();
	}


	public override void Reset()
	{
		base.Reset();
		LandingPlatform landingPlatform = consumer.GetConsumerLandingPlatform();
		Vector3 position = landingPlatform.GetLandingPosition();

		//Move
		actionList.PushAction(new LookAtAction(position, shipObject, true));
		actionList.PushAction(new AlignShipForDeliveryAction(shipObject, carryObject, resourceData, landingPlatform));

		//Deliver
		actionList.PushAction(new AnimateLoadingPlatformOpenAction(landingPlatform));
		actionList.PushAction(new ParentFreightToPlatformAction(landingPlatform, carryObject, resourceData));
		actionList.PushAction(new AnimateLoadingPlatformCloseAction(landingPlatform));
		actionList.PushAction(new SyncAction());
		actionList.PushAction(new DestoryFreightAction(landingPlatform));
	}

	public override void OnInitialise()
	{
		base.OnInitialise();
		if (!CheckShipIsValid() || !CheckProducerIsValid())
		{
			failed = true;
			actionList.Clear();
			isFinished = true;
		}

	}

	private bool CheckShipIsValid()
	{
		if (!carryObject.HasFreightOfResource(resourceData))
		{
			NotificationManager.instance.AddNotification(shipObject.shipName, $"Cannot start Delivery because {shipObject.name} does not have any {resourceData.resourceName}");
			return false;
		}
		return true;
	}

	private bool CheckProducerIsValid()
	{
		if (!consumer.AcceptsResource(resourceData))
		{
			NotificationManager.instance.AddNotification(shipObject.shipName, $"Cannot start Delivery because {consumer.GetType().ToString()} does accept {resourceData.resourceName}");
			return false;
		}
		return true;
	}

	public override void OnFinish()
	{
		WalletManager.instance.CreateAndAddSellTransaction(resourceData.price, consumer as Building, shipObject, resourceData);
	}
}
