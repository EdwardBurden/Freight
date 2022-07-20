using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TODO
[CreateAssetMenu(menuName = "ScriptableObject/MoveActionDefintion")]
public class MoveActionDefintion : ShipActionDefintion
{
	public override bool CanShowOption(ShipActionSelectionInfo shipActionSelectionInfo)
	{
		return true;
	}

	public override void OnOptionSelected(ShipActionSelectionInfo shipActionSelectionInfo, Action<ShipAction> callback)
	{
		callback.Invoke(new MoveAction(this, shipActionSelectionInfo.ship, shipActionSelectionInfo.clickPosition));
	}
}
