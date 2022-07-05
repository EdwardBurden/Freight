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
		Vector3 position = target.GetLandingPosition();
		Quaternion rotation = target.GetLandingRotation();

		//Move
		AnimateShipLookAtAction animateShipLookAtAction = new AnimateShipLookAtAction();
		AnimateShipIdleAction animateShipIdleAction = new AnimateShipIdleAction();
		LookAtAction lookAtAction = new LookAtAction(position, 80, gameObject);
		SyncAction syncAction = new SyncAction();
		MoveToAction moveToAction = new MoveToAction(position, 5, gameObject, true);

		//Collect - to implement
		RotateToAction rotateToAction = new RotateToAction(rotation, 80, gameObject, true);
		SpawnFreightAction spawnFreightAction = new SpawnFreightAction();
		AnimateShipCollectAction animateShipCollectAction = new AnimateShipCollectAction();
		AnimateLoadingPlatformOpenAction animateLoadingPlatformOpenAction = new AnimateLoadingPlatformOpenAction();
		ParentFreightAction connectFreightToShipAction = new ParentFreightAction();
		AnimateLoadingPlatformCloseAction animateLoadingPlatformCloseAction = new AnimateLoadingPlatformCloseAction();
		

		actionList.PushAction(animateShipLookAtAction);
		actionList.PushAction(lookAtAction);
		actionList.PushAction(syncAction);
		actionList.PushAction(moveToAction);
		actionList.PushAction(animateShipIdleAction);


		actionList.PushAction(rotateToAction);
		//actionList.PushAction(spawnFreightAction);
		//actionList.PushAction(animateShipCollectAction);
		//actionList.PushAction(animateLoadingPlatformOpenAction);
		//actionList.PushAction(animateShipIdleAction);
		//actionList.PushAction(connectFreightToShipAction);
		//actionList.PushAction(animateLoadingPlatformCloseAction);
		//actionList.PushAction(animateShipIdleAction);


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
