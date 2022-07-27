using UnityEngine;

public class MoveAction : ShipAction
{
	private Vector3 target;
	public MoveAction(ShipActionDefintion shipActionDefintion, ShipObject shipObject, Vector3 target) : base(shipActionDefintion, shipObject)
	{
		this.target = target;
		Reset();
	}


	public override void Reset()
	{
		base.Reset();
		actionList.PushAction(new LookAtAction(target, shipObject, true));
		actionList.PushAction(new MoveToAction(target, shipObject, true));
	}
}