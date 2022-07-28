using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TODO
[CreateAssetMenu(menuName = "ScriptableObject/MoveActionDefintion")]
public class MoveActionDefintion : ShipActionDefintion
{
	[SerializeReference]
	private MoveActionPreview previewMoveActionPrefab;

	public override bool CanShowOption(RightClickContextActionInfo shipActionSelectionInfo)
	{
		return true;
	}

	public override ActionPreview CreateActionPreview(ShipObject ship, Action<ShipAction> callback)
	{
		MoveActionPreview previewMoveAction = Instantiate(previewMoveActionPrefab);
		previewMoveAction.Open(ship, (postion) => OnPreviewClick(ship, postion, callback));
		return previewMoveAction;
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
