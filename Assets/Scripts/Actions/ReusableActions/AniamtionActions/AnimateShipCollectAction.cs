using ActionLists;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateShipCollectAction : AnimateAction
{
	public AnimateShipCollectAction() : base(null) { }
	public override void OnUpdate()
	{
		isFinished = true;
	}
}
