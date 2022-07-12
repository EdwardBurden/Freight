using ActionLists;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverAction : HierarchicalAction
{
	private ShipObject shipObject;
	private CarryObject carryObject;
	private IConsumer consumer;
	private ResourceData resourceData;
	public DeliverAction(ShipObject shipObject, CarryObject carryObject, IConsumer target, ResourceData resourceTarget) : base()
	{
		this.shipObject = shipObject;
		this.carryObject = carryObject;
		this.consumer = target;
		this.resourceData = resourceTarget;
		CreateActions();
	}


	private void CreateActions()
	{
		LandingPlatform landingPlatform = consumer.GetLandingPlatform();
		Vector3 position = landingPlatform.GetLandingPosition();

		//Move
		MoveAction moveAction = new MoveAction(shipObject, position);
		actionList.PushAction(moveAction);

		//Deliver
		actionList.PushAction(new AlignShipForDeliveryAction(shipObject, carryObject, resourceData, landingPlatform)); //block
		actionList.PushAction(new AnimateShipLandAction(null)); //not block
		actionList.PushAction(new AnimateLoadingPlatformOpenAction(landingPlatform));//not block
		actionList.PushAction(new SyncAction());
		actionList.PushAction(new DelayAction(2));

		actionList.PushAction(new AnimateShipDeliverAction());//block
		actionList.PushAction(new ParentFreightToPlatformAction(landingPlatform, carryObject, resourceData));//block
		actionList.PushAction(new AnimateShipIdleAction()); //not block
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
			//Alert User
			actionList.Clear();
			isFinished = true;
		}

	}

	private bool CheckShipIsValid()
	{
		//check ship has the resource to deliver
		return true;

	}

	private bool CheckProducerIsValid()
	{
		//check consumer accepts the resource
		return true;

	}
}
