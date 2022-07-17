using ActionLists;
using System;

public abstract class ShipAction : HierarchicalAction
{
	public bool failed = false;
	public ShipActionDefintion actionDefintion;
	public ShipObject shipObject;

	public ShipAction(ShipActionDefintion shipActionDefintion, ShipObject shipObject) : base()
	{
		actionDefintion = shipActionDefintion;
		this.shipObject = shipObject;
	}

	public override void OnUpdate()
	{
		if (failed)
		{
			return;
		}
		base.OnUpdate();
	}

	internal void OnFinish()
	{
		//throw new NotImplementedException();
	}
}
