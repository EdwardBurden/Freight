using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleShip : Ship
{
	ShipOrder shipAction;

	public override void PushAction(ShipOrder shipAction)
	{
		this.shipAction = shipAction;
	}

	public override void SetAction(ShipOrder shipAction)
	{
		this.shipAction = shipAction;
	}

	private void Update() // temp for now, needs to be able to use finsih and init same as freightship
	{
		//TODO
		/*
		if (shipAction == null)
		{
			return;
		}

		bool blocked = false;
		while (!blocked)
		{
			if (!shipAction.started)
			{
				shipAction.OnInitialise();
			}
			shipAction.OnUpdate();
			blocked = shipAction.isBlocking;
			if (shipAction.isFinished || shipAction.failed)
			{
				shipAction = null;
			}
		}*/
	}
}
