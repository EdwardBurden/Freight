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
		actionList.PushAction(new RotateToAction(rotation, 80, gameObject, true));
		actionList.PushAction(new AnimateShipDeliverAction());
		actionList.PushAction(new AnimateLoadingPlatformOpenAction());
		actionList.PushAction(new SyncAction());
		actionList.PushAction(new DelayAction(2));
		actionList.PushAction(new ParentFreightToPlatformAction(landingPlatform, gameObject, resourceData));
		actionList.PushAction(new AnimateShipIdleAction());
		actionList.PushAction(new AnimateLoadingPlatformCloseAction());
		actionList.PushAction(new DestoryFreightAction(landingPlatform));

		actionList.PushAction(new DelayAction(2));
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
