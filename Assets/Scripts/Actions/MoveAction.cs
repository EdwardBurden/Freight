using ActionLists;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : HierarchicalAction
{
	public MoveAction(GameObject gameObject, Vector3 target) : base()
	{
		actionList.PushAction(new AnimateShipLookAtAction());
		actionList.PushAction(new LookAtAction(target, 80, gameObject, true));
		actionList.PushAction(new SyncAction());
		actionList.PushAction(new MoveToAction(target, 5, gameObject, true));
		actionList.PushAction(new AnimateShipIdleAction());
	}
}
