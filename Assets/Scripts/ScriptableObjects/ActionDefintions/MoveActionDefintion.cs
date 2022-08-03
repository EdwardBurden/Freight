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

	public override ActionPreview CreateActionPreview(Ship ship, Action<ShipAction> callback)
	{
		MoveActionPreview previewMoveAction = Instantiate(previewMoveActionPrefab);
		previewMoveAction.Open(ship, (postion) => OnPreviewClick(ship, postion, callback));
		return previewMoveAction;
	}

	private void OnPreviewClick(Ship shipObject, Vector3 position, Action<ShipAction> callback)
	{
		MoveAction moveAction = new MoveAction(this, shipObject, position);
		callback.Invoke(moveAction);
	}

	public override void ActionSelected(Ship shipObject, InteractableObject interactableObject, Vector3 position, Action<ShipAction> callback)
	{
		MoveAction moveAction = new MoveAction(this, shipObject, position);
		callback.Invoke(moveAction);
	}
}
