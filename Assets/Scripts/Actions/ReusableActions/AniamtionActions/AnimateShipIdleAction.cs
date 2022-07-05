using ActionLists;
using UnityEngine;

public class AnimateShipIdleAction : AnimateAction
{
	public override void OnUpdate()
	{
		isFinished = true;
	}
}
