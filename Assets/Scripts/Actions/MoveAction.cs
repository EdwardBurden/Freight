using ActionLists;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : HierarchicalAction
{
	public MoveAction(GameObject gameObject, Vector3 target) : base()
	{
		AnimateShipLookAtAction animateShipLookAtAction = new AnimateShipLookAtAction();
		LookAtAction lookAtAction = new LookAtAction(target, 80, gameObject, true);
		MoveToAction moveToAction = new MoveToAction(target, 5, gameObject, true);
		AnimateShipIdleAction animateShipIdleAction = new AnimateShipIdleAction();

		actionList.PushAction(animateShipLookAtAction);
		actionList.PushAction(lookAtAction);
		actionList.PushAction(new SyncAction());
		actionList.PushAction(moveToAction);
		actionList.PushAction(animateShipIdleAction);
	}


}
