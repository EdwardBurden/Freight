using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateShipLandAction : AnimateAction
{
	public AnimateShipLandAction(Animator animator) : base(animator) { }

	public override void OnUpdate()
	{
		isFinished = true;
	}
}
