using ActionLists;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectAction : HierarchicalAction
{
	public CollectAction(GameObject gameObject, IProducer target, ResourceData resourceTarget) : base()
	{
		//Checks first
		//CheckShiphasSpace //exit out if not/throw some alarm to the user throughj notification
		//Check IProducer makes that resourceTarget

		//AnimateShipLookAtAction
		//LookAtAction

		//MoveToAction

		//RotateIntoOrientation action - get orientation from IProducer

		//AnimateShipIdleAction

		//AnimateShipCollectAction

		//LoadAction- resourceTarget

		//AnimateProducer

		//AnimateShipIdleAction
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
