using ActionLists;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverAction : ShipAction
{
	private CarryObject carryObject;
	private IConsumer consumer;
	private ResourceData resourceData;

	public DeliverAction(ShipActionDefintion shipActionDefintion, ShipObject shipObject, IConsumer target, ResourceData resourceTarget) : base(shipActionDefintion, shipObject)
	{
		this.carryObject = shipObject.GetComponent<CarryObject>();
		this.consumer = target;
		this.resourceData = resourceTarget;
		CreateActions();
	}


	private void CreateActions()
	{
		LandingPlatform landingPlatform = consumer.GetLandingPlatform();
		Vector3 position = landingPlatform.GetLandingPosition();

		//Move
		actionList.PushAction(new LookAtAction(position, shipObject, true));
		actionList.PushAction(new SyncAction());
		actionList.PushAction(new AlignShipForDeliveryAction(shipObject, carryObject, resourceData, landingPlatform)); //block

		//Deliver
		actionList.PushAction(new AnimateLoadingPlatformOpenAction(landingPlatform));//not block
		actionList.PushAction(new SyncAction());
		actionList.PushAction(new DelayAction(2));

		actionList.PushAction(new ParentFreightToPlatformAction(landingPlatform, carryObject, resourceData));//block
		actionList.PushAction(new AnimateLoadingPlatformCloseAction(landingPlatform)); //not block
		actionList.PushAction(new SyncAction());
		actionList.PushAction(new DelayAction(2));
		actionList.PushAction(new DestoryFreightAction(landingPlatform));
	}

	public override void OnInitialise()
	{
		base.OnInitialise();
		if (!CheckShipIsValid() || !CheckProducerIsValid())
		{
			actionList.Clear();
			isFinished = true;
		}

	}

	private bool CheckShipIsValid()
	{
		if (!carryObject.HasFreightOfResource(resourceData))
		{
			NotificationManager.instance.AddNotification(shipObject, $"Cannot start Delivery because {shipObject.name} does not have any {resourceData.resourceName}");
			return false;
		}
		return true;
	}

	private bool CheckProducerIsValid()
	{
		if (!consumer.AcceptsResource(resourceData))
		{
			NotificationManager.instance.AddNotification(shipObject, $"Cannot start Delivery because {consumer.GetType().ToString()} does accept {resourceData.resourceName}");
			return false;
		}
		return true;
	}
}
