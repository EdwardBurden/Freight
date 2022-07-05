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
		MoveAction moveAction = new MoveAction(gameObject, position);
		actionList.PushAction(moveAction);

		//Collect - to implement
		RotateToAction rotateToAction = new RotateToAction(rotation, 80, gameObject, true);
		//Remove freight from stockpile of producer
		SpawnFreightAction spawnFreightAction = new SpawnFreightAction(resourceData, landingPlatform);
		AnimateShipCollectAction animateShipCollectAction = new AnimateShipCollectAction();
		AnimateLoadingPlatformOpenAction animateLoadingPlatformOpenAction = new AnimateLoadingPlatformOpenAction();
		ParentFreightAction connectFreightToShipAction = new ParentFreightAction(landingPlatform , gameObject);
		AnimateShipIdleAction animateShipIdleAction = new AnimateShipIdleAction();
		AnimateLoadingPlatformCloseAction animateLoadingPlatformCloseAction = new AnimateLoadingPlatformCloseAction();

		actionList.PushAction(rotateToAction);
		actionList.PushAction(spawnFreightAction);
		actionList.PushAction(animateShipCollectAction);
		actionList.PushAction(animateLoadingPlatformOpenAction);
		actionList.PushAction(new SyncAction());
		actionList.PushAction(connectFreightToShipAction);
		actionList.PushAction(animateShipIdleAction);
		actionList.PushAction(animateLoadingPlatformCloseAction);

		DelayAction delayAction = new DelayAction(2);
		actionList.PushAction(delayAction);
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
