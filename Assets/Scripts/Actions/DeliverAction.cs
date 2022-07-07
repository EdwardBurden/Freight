using ActionLists;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverAction : HierarchicalAction
{
	private GameObject gameObject;
	private IConsumer consumer;
	private ResourceData resourceData;
	public DeliverAction(GameObject gameObject, IConsumer target, ResourceData resourceTarget) : base()
	{
		this.gameObject = gameObject;
		this.consumer = target;
		this.resourceData = resourceTarget;
		CreateActions();
	}


	private void CreateActions()
	{
		LandingPlatform landingPlatform = consumer.GetLandingPlatform();
		Vector3 position = landingPlatform.GetLandingPosition();
		Quaternion rotation = landingPlatform.GetLandingRotation();

		//Move
		MoveAction moveAction = new MoveAction(gameObject, position);
		actionList.PushAction(moveAction);

		//Deliver
		actionList.PushAction(new RotateToAction(rotation, 80, gameObject, true)); //block
		actionList.PushAction(new AnimateShipLandAction(null)); //not block
		actionList.PushAction(new AnimateLoadingPlatformOpenAction(landingPlatform));//not block
		actionList.PushAction(new SyncAction());
		actionList.PushAction(new DelayAction(2));

		actionList.PushAction(new AnimateShipDeliverAction());//block
		actionList.PushAction(new ParentFreightToPlatformAction(landingPlatform, gameObject, resourceData));//block
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
