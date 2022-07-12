using ActionLists;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : HierarchicalAction
{
	public MoveAction(ShipObject shipObject, Vector3 target) : base()
	{
		actionList.PushAction(new AnimateShipLookAtAction());
		actionList.PushAction(new LookAtAction(target, shipObject, true));
		actionList.PushAction(new SyncAction());
		actionList.PushAction(new MoveToAction(target, shipObject, true));
		actionList.PushAction(new AnimateShipIdleAction());
	}

	public override void OnInitialise()
	{
		base.OnInitialise();
		//If alreayd here quit
	}
}
