using ActionLists;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverAction : HierarchicalAction
{
	public DeliverAction(GameObject gameObject, IConsumer target, ResourceData resourceTarget) :base()
	{
		//Checks first
		//CheckGameobjecthasResource //exit out if not/throw some alarm to the user throughj notification
		//check iConsumer accepts resource

		//AnimateShipLookAtAction
		//LookAtAction

		//MoveToAction

		//RotateIntoOrientation action - get orientation from IConsumer

		//AnimateShipIdleAction

		//AnimateConsumerShowFreightAction

		//AnimateShipDeliverAction

		//UnloadAction- resourceTarget

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
		//check ship has the resource to deliver
		return true;

	}

	private bool CheckProducerIsValid()
	{
		//check consumer accepts the resource
		return true;

	}
}
