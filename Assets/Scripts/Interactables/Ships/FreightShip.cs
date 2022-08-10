using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShipActionList))]
public class FreightShip : Ship
{
	[SerializeField]
	private ShipActionList actionList;

	private void Awake()
	{
		actionList = GetComponent<ShipActionList>();
	}

	public override void PushAction(ShipAction shipAction)
	{
		actionList.PushAction(shipAction);
	}

	public override void SetAction(ShipAction shipAction) //TODO
	{
		actionList.ClearActions();
		actionList.paused = false;
		actionList.repeat = false;
		actionList.PushAction(shipAction);
	}
}
