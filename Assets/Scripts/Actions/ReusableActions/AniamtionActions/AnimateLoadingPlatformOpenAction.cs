using ActionLists;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateLoadingPlatformOpenAction : AnimateAction
{
	public override void OnUpdate()
	{
		isFinished = true;
	}
}
