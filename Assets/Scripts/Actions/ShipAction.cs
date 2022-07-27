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

	public virtual void OnFinish()
	{
		//throw new NotImplementedException();
	}

	public virtual void OnRemove()
	{
		//throw new NotImplementedException();
	}
}
