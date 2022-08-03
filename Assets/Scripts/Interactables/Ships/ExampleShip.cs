using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleShip : Ship
{
	ShipAction shipAction;

	public override void PushAction(ShipAction shipAction)
	{
		this.shipAction = shipAction;
	}

	public override void SetAction(ShipAction shipAction)
	{
		this.shipAction = shipAction;
	}

	private void Update()
	{
		if (shipAction == null)
		{
			return;
		}

		bool blocked = false;
		while (!blocked)
		{
			if (!shipAction.isInitialised)
			{
				shipAction.OnInitialise();
			}
			shipAction.OnUpdate();
			blocked = shipAction.isBlocking;
			if (shipAction.isFinished || shipAction.failed)
			{
				shipAction = null;
			}
		}
	}
}
