using UnityEngine;

[RequireComponent(typeof(ShipOrderQueue))]
[RequireComponent(typeof(CarryObject))]
public class FreightShip : Ship
{
	
	private ShipOrderQueue actionList;

	private void Awake()
	{
		actionList = GetComponent<ShipOrderQueue>();
	}

	public override void PushAction(ShipOrder shipAction)
	{
		actionList.PushAction(shipAction);
	}

	public override void SetAction(ShipOrder shipAction)
	{
		actionList.ClearActions();
		actionList.paused = false;
		actionList.repeat = false;
		actionList.PushAction(shipAction);
	}
}
