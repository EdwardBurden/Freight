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
		AnimateShipLookAtAction animateShipLookAtAction = new AnimateShipLookAtAction(position, gameObject);
		AnimateShipIdleAction animateShipIdleAction = new AnimateShipIdleAction(gameObject);
		LookAtAction lookAtAction = new LookAtAction(position, 80, gameObject);
		SyncAction syncAction = new SyncAction();
		MoveToAction moveToAction = new MoveToAction(position, 5, gameObject, true);

		actionList.PushAction(animateShipLookAtAction);
		actionList.PushAction(lookAtAction);
		actionList.PushAction(syncAction);
		actionList.PushAction(moveToAction);
		actionList.PushAction(animateShipIdleAction);
		//RotateIntoOrientation action - get orientation from IProducer
		//AnimateShipIdleAction
		//AnimateShipCollectAction
		//LoadAction- resourceTarget
		//AnimateProducer
		//AnimateShipIdleAction

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
