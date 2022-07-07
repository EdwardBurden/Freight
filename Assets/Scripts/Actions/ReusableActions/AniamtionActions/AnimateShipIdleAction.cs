using ActionLists;
using UnityEngine;

public class AnimateShipIdleAction : AnimateAction
{
	public AnimateShipIdleAction() : base(null) { }
	public override void OnUpdate()
	{
		isFinished = true;
	}
}
