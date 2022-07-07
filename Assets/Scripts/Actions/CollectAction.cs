using ActionLists;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectAction : HierarchicalAction
{
	private CarryObject carryObject;
	private IProducer producer;
	private ResourceData resourceData;


	public CollectAction(CarryObject carryObject, IProducer target, ResourceData resourceTarget) : base()
	{
		this.carryObject = carryObject;
		this.producer = target;
		this.resourceData = resourceTarget;
		CreateActions();
	}

	private void CreateActions()
	{
		LandingPlatform landingPlatform = producer.GetLandingPlatform();
		Vector3 position = landingPlatform.GetLandingPosition();
		Quaternion rotation = landingPlatform.GetLandingRotation();

		//Move
		actionList.PushAction(new MoveAction(carryObject.gameObject, position));

		//Collect
		actionList.PushAction(new RotateToAction(rotation, 80, carryObject.gameObject, true));//block
		actionList.PushAction(new SpawnFreightAction(resourceData, landingPlatform)); //block
		actionList.PushAction(new AnimateShipLandAction(null)); //not block
		actionList.PushAction(new AnimateLoadingPlatformOpenAction(landingPlatform));    //not block
		actionList.PushAction(new SyncAction());
		actionList.PushAction(new DelayAction(2));

		actionList.PushAction(new AnimateShipCollectAction()); //block
		actionList.PushAction(new ParentFreightToShipAction(landingPlatform, carryObject.gameObject)); //block
		actionList.PushAction(new AnimateShipIdleAction()); //not block
		actionList.PushAction(new AnimateLoadingPlatformCloseAction(landingPlatform)); //not block
		actionList.PushAction(new SyncAction());
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
		//check ship has space to collect
		return true;

	}

	private bool CheckProducerIsValid()
	{
		//check producer has the resource type
		return true;

	}
}
