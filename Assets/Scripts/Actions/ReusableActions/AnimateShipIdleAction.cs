using ActionLists;
using UnityEngine;

public class AnimateShipIdleAction : GameObjectAction
{
	public AnimateShipIdleAction(GameObject gameObject) : base(gameObject)
	{

	}

	public override void OnUpdate()
	{

		isFinished = true;
		//stop playing when target is reached
	}

	private void AnimateShipIdle()
	{
		//play animation that angles them toward target
	}
}
