using ActionLists;
using UnityEngine;

public class AnimateShipLookAtAction : GameObjectAction
{

	public AnimateShipLookAtAction(Vector3 target, GameObject gameObject) : base(gameObject)
	{

	}

	public override void OnUpdate()
	{

		isFinished = true;
		//stop playing when target is reached
	}

	private void AnimateShipLookAt()
	{
	//play animation that angles them toward target
	}
}
