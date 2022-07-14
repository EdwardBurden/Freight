using ActionLists;
using System;

public abstract class ShipAction : HierarchicalAction
{
	protected ShipActionDefintion actionDefintion;
	public ShipObject shipObject;

	public ShipAction(ShipActionDefintion shipActionDefintion, ShipObject shipObject) : base()
	{
		actionDefintion = shipActionDefintion;
		this.shipObject = shipObject;
	}
}
