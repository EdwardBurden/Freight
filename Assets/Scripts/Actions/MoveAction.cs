using ActionLists;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : HierarchicalAction
{
	public MoveAction(GameObject gameObject, Vector3 target) : base()
	{
		AnimateShipLookAtAction animateShipLookAtAction = new AnimateShipLookAtAction();
		AnimateShipIdleAction animateShipIdleAction = new AnimateShipIdleAction();
		LookAtAction lookAtAction = new LookAtAction(target, 80, gameObject);
		SyncAction syncAction = new SyncAction();
		MoveToAction moveToAction = new MoveToAction(target, 5, gameObject, true);

		actionList.PushAction(animateShipLookAtAction);
		actionList.PushAction(lookAtAction);
		actionList.PushAction(syncAction);
		actionList.PushAction(moveToAction);
		actionList.PushAction(animateShipIdleAction);
	}


}
