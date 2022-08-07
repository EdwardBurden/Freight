using ActionLists;
using System;

public abstract class ShipAction : HierarchicalAction
{
	public bool failed = false;
	public ShipActionDefintion actionDefintion;
	public Ship shipObject;

	public ShipAction(ShipActionDefintion shipActionDefintion, Ship shipObject) : base()
	{
		actionDefintion = shipActionDefintion;
		this.shipObject = shipObject;
	}

	public virtual void Reset()
	{
		isFinished = false;
		isInitialised = false;
		failed = false;
		actionList.Clear();
	}

	public override void OnUpdate()
	{
		if (failed)
		{
			return;
		}
		base.OnUpdate();
	}

	public abstract void OnFinish();

	/*public virtual void OnRemove()
	{
		//throw new NotImplementedException();
	}*/
}
