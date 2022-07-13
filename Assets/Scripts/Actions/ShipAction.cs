using ActionLists;
using System;

public abstract class ShipAction : HierarchicalAction
{
	protected ShipActionDefintion actionDefintion;

	public ShipAction(ShipActionDefintion shipActionDefintion) : base()
	{
		actionDefintion = shipActionDefintion;
	}
}
