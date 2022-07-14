using UnityEngine;

public class MoveAction : ShipAction
{
	public MoveAction(ShipActionDefintion shipActionDefintion, ShipObject shipObject, Vector3 target) : base(shipActionDefintion , shipObject)
	{
		actionList.PushAction(new LookAtAction(target, shipObject, true));
		actionList.PushAction(new SyncAction());
		actionList.PushAction(new MoveToAction(target, shipObject, true));
	}
}
