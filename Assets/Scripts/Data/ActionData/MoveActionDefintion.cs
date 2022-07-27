using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TODO
[CreateAssetMenu(menuName = "ScriptableObject/MoveActionDefintion")]
public class MoveActionDefintion : ShipActionDefintion
{
	public override bool CanShowOption(RightClickContextActionInfo shipActionSelectionInfo)
	{
		return true;
	}

	public override void OnOptionSelectedFromActionsListPanel(ShipObject ship, Action<ShipAction> callback)
	{
		throw new NotImplementedException();
	}

	public override void OnOptionSelectedFromRightClickMenu(RightClickContextActionInfo shipActionSelectionInfo, Action<ShipAction> callback)
	{
		callback.Invoke(new MoveAction(this, shipActionSelectionInfo.ship, shipActionSelectionInfo.clickPosition));
	}
}
