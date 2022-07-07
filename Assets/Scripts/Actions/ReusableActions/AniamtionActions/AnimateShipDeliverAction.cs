using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateShipDeliverAction : AnimateAction
{
	public AnimateShipDeliverAction() : base(null) { }
	public override void OnUpdate()
	{
		isFinished = true;
	}
}
