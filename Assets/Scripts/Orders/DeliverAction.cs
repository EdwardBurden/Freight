using ActionLists;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverAction : ShipOrder
{
	private CarryObject carryObject;
	private IConsumer consumer;
	private ResourceDefintion resourceData;

	public DeliverAction(ShipActionDefintion shipActionDefintion, Ship shipObject, IConsumer target, ResourceDefintion resourceTarget) : base(shipActionDefintion, shipObject)
	{
		this.carryObject = shipObject.GetComponent<CarryObject>();
		this.consumer = target;
		this.resourceData = resourceTarget;
	}


	public override void Setup()
	{
		base.Setup();
		LandingPlatform landingPlatform = consumer.GetConsumerLandingPlatform();
		Vector3 position = landingPlatform.GetLandingPosition();

		//Move
		childList.PushAction(new LookAtAction(position, shipObject, true));
		childList.PushAction(new AlignShipForDeliveryAction(shipObject, carryObject, resourceData, landingPlatform));

		//Deliver
		childList.PushAction(new AnimateLoadingPlatformOpenAction(landingPlatform));
		childList.PushAction(new ParentFreightToPlatformAction(landingPlatform, carryObject, resourceData));
		childList.PushAction(new AnimateLoadingPlatformCloseAction(landingPlatform));
		childList.PushAction(new SyncAction());
		childList.PushAction(new DestoryFreightAction(landingPlatform));
	}

	public override void Start()
	{
		base.Start();
		if (!CheckShipIsValid() || !CheckProducerIsValid())
		{
			RaiseFailEvent("Deliver error");
		}

	}

	private bool CheckShipIsValid()
	{
		if (!carryObject.HasFreightOfResource(resourceData))
		{
			//Notifications.instance.AddNotification(shipObject.shipName, $"Cannot start Delivery because {shipObject.name} does not have any {resourceData.resourceName}");
			return false;
		}
		return true;
	}

	private bool CheckProducerIsValid()
	{
		if (!consumer.AcceptsResource(resourceData))
		{
			//Notifications.instance.AddNotification(shipObject.shipName, $"Cannot start Delivery because {consumer.GetType().ToString()} does accept {resourceData.resourceName}");
			return false;
		}
		return true;
	}

	public override void Finish()
	{
		int price = consumer.GetBuyPrice(resourceData);
		Wallet wallet = GameObject.FindObjectOfType<Wallet>(); //temp fx
		wallet.CreateAndAddSellTransaction(price, consumer as Building, shipObject, resourceData);
	}
}
