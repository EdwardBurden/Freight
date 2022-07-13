using ActionLists;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverAction : ShipAction
{
	private ShipObject shipObject;
	private CarryObject carryObject;
	private IConsumer consumer;
	private ResourceData resourceData;
	public DeliverAction(ShipActionDefintion shipActionDefintion, ShipObject shipObject, IConsumer target, ResourceData resourceTarget) : base(shipActionDefintion)
	{
		this.shipObject = shipObject;
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
		actionList.PushAction(new MoveToAction(position, shipObject, true));

		//Deliver
		actionList.PushAction(new AlignShipForDeliveryAction(shipObject, carryObject, resourceData, landingPlatform)); //block
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
			//TODO
			//Alert User
			actionList.Clear();
			isFinished = true;
		}

	}

	private bool CheckShipIsValid()
	{
		//TODO
		//check ship has the resource to deliver
		return true;

	}

	private bool CheckProducerIsValid()
	{
		//TODO
		//check consumer accepts the resource
		return true;

	}
}
