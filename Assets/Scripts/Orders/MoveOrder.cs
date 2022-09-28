using UnityEngine;

public class MoveOrder : ShipOrder
{
	private Vector3 target;

	public MoveOrder(ShipActionDefintion shipActionDefintion, Ship shipObject, Vector3 target) : base(shipActionDefintion, shipObject)
	{
		this.target = target;
	}

	public override void Setup()
	{
		base.Setup();
		childList.PushAction(new LookAtAction(target, shipObject, true));
		childList.PushAction(new MoveToAction(target, shipObject, true));
	}
}