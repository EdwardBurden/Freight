using ActionLists;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : HierarchicalAction
{
	public MoveAction(GameObject gameObject, Transform target) : base()
	{
		//If landed
		//add levitate aniamtion and move the ship up to correct level


		//else ship is in the air and ready to move
		AnimateShipEnginesLookAtAction animateShipEnginesLookAtAction = new AnimateShipEnginesLookAtAction(target, gameObject);
		//	AnimateShipEnginesIdleAction animateShipEnginesIdleAction = new AnimateShipEnginesIdleAction(target, gameObject);
		LookAtAction lookAtAction = new LookAtAction(target.position, 80, gameObject);
		SyncAction syncAction = new SyncAction();
		MoveToAction moveToAction = new MoveToAction(target.position, 5, gameObject, true);

		actionList.PushAction(animateShipEnginesLookAtAction);
		actionList.PushAction(lookAtAction);
		actionList.PushAction(syncAction);
		actionList.PushAction(moveToAction);
		//	actionList.PushAction(animateShipEnginesIdleAction);
	}


}
