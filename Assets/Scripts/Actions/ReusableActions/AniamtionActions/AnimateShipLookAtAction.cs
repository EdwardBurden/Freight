using ActionLists;
using UnityEngine;

public class AnimateShipLookAtAction : AnimateAction
{
	public AnimateShipLookAtAction() : base(null) { }
	public override void OnUpdate()
	{
		isFinished = true;
	}
}
