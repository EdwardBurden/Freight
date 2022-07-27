using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TODO
[CreateAssetMenu(menuName = "ScriptableObject/MoveActionDefintion")]
public class MoveActionDefintion : ShipActionDefintion
{
	[SerializeReference]
	private PreviewMoveAction previewMoveActionPrefab;

	public override bool CanShowOption(RightClickContextActionInfo shipActionSelectionInfo)
	{
		return true;
	}

	public override void OnOptionSelectedFromActionsListPanel(ShipObject ship, Action<ShipAction> callback)
	{
		//open Spawn a prefab with a class that that subscribes to MouseInteractionTracker
		//     it first stops ShipSelectionManager
		//		and draws a line from the ship to where the user mouse is
		//		when they click it gets the postion and returns here
		//		and re enables ShipSelectionManager
		//	throw new NotImplementedException();
		PreviewMoveAction previewMoveAction = Instantiate(previewMoveActionPrefab);
		previewMoveAction.Open(ship, (postion) => OnPreviewClick(ship, postion, callback));
		//PreviewMoveAction.instance.Open(ship, (postion) => OnPreviewClick(ship, postion, callback));
	}

	private void OnPreviewClick(ShipObject shipObject, Vector3 position, Action<ShipAction> callback)
	{
		callback.Invoke(new MoveAction(this, shipObject, position));

	}

	public override void OnOptionSelectedFromRightClickMenu(RightClickContextActionInfo shipActionSelectionInfo, Action<ShipAction> callback)
	{
		callback.Invoke(new MoveAction(this, shipActionSelectionInfo.ship, shipActionSelectionInfo.clickPosition));
	}
}
