using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShipOrderQueue))]
public class FreightShip : Ship
{
	[SerializeField]
	private ShipOrderQueue actionList;

	private void Awake()
	{
		actionList = GetComponent<ShipOrderQueue>();
	}

	public override void PushAction(ShipOrder shipAction)
	{
		actionList.PushAction(shipAction);
	}

	public override void SetAction(ShipOrder shipAction) //TODO
	{
		actionList.ClearActions();
		actionList.paused = false;
		actionList.repeat = false;
		actionList.PushAction(shipAction);
	}
}
