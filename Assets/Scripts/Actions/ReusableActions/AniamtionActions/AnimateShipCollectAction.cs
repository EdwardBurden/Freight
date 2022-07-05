using ActionLists;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateShipCollectAction : AnimateAction
{
	public override void OnUpdate()
	{
		isFinished = true;
	}
}
