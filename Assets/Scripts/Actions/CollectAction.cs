using ActionLists;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectAction : HierarchicalAction
{
	private GameObject gameObject;
	private IProducer producer;
	private ResourceData resourceData;


	public CollectAction(GameObject gameObject, IProducer target, ResourceData resourceTarget) : base()
	{
		this.gameObject = gameObject;
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
		actionList.PushAction(new MoveAction(gameObject, position));

		//Collect
		actionList.PushAction(new RotateToAction(rotation, 80, gameObject, true));
		actionList.PushAction(new SpawnFreightAction(resourceData, landingPlatform));
		actionList.PushAction(new AnimateShipCollectAction());
		actionList.PushAction(new AnimateLoadingPlatformOpenAction());
		actionList.PushAction(new SyncAction());
		actionList.PushAction(new DelayAction(2)); //for testing
		actionList.PushAction(new ParentFreightToShipAction(landingPlatform, gameObject));
		actionList.PushAction(new AnimateShipIdleAction());
		actionList.PushAction(new AnimateLoadingPlatformCloseAction());

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
