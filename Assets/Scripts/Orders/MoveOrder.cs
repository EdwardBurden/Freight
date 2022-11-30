using UnityEngine;

public class MoveOrder : ShipOrder
{
	private Vector3 target;
	private Vector3 height = new Vector3(0, 5, 0);

	public MoveOrder(ShipActionDefintion shipActionDefintion, Ship shipObject, Vector3 target) : base(shipActionDefintion, shipObject)
	{
		this.target = target;
	}

	public override void Setup()
	{
		base.Setup();
		childList.PushAction(new LookAtAction(target + height, shipObject));
		childList.PushAction(new MoveToAction(target + height, shipObject, true));
	}
}