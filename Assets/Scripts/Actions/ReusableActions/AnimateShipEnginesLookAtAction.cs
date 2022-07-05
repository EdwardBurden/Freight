using ActionLists;
using UnityEngine;

public class AnimateShipEnginesLookAtAction : GameObjectAction
{

	public AnimateShipEnginesLookAtAction(Transform transform, GameObject gameObject) : base(gameObject)
	{

	}

	public override void OnUpdate()
	{

		isFinished = false;
		//stop playing when target is reached
	}

	private void AnimateShipEngines()
	{
	//play animation that angles them toward target
	}
}
