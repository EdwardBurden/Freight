using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/MoveActionDefintion")]
public class MoveActionDefintion : ShipActionDefintion
{
	[SerializeReference]
	private MoveActionPreview previewMoveActionPrefab;

	public override bool CanBeCreated(Ship shipObject, InteractableObject interactableObject, Vector3 postion)
	{
		return true;
	}

	public override void CreateActionPreview(Ship ship, Action<ShipOrder> callback)
	{
		MoveActionPreview previewMoveAction = ActionPreviewController.instance.CreateActionPreview(ship, previewMoveActionPrefab) as MoveActionPreview;
		previewMoveAction.callback = (postion) => OnPreviewClick(ship, postion, callback);
	}

	private void OnPreviewClick(Ship shipObject, Vector3 position, Action<ShipOrder> callback)
	{
		MoveOrder moveAction = new MoveOrder(this, shipObject, position);
		callback.Invoke(moveAction);
	}

	public override void ActionSelected(Ship shipObject, InteractableObject interactableObject, Vector3 position, Action<ShipOrder> callback)
	{
		MoveOrder moveAction = new MoveOrder(this, shipObject, position);
		callback.Invoke(moveAction);
	}
}
