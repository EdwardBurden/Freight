using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class MoveActionPreview : ActionPreview //or created in the scen froma prefab and killed when doen, can decide later
{
	public Action<Vector3> callback;

	public override void OnRightClickInteractable(InteractableObject interactableObject, Vector3 position)
	{
		ActionPreviewController.instance.FinishActionPreview();
	}

	public override void OnLeftClickInteractable(InteractableObject interactableObject, Vector3 position)
	{
		RaiseFinsihedEvent();
		callback?.Invoke(position);
	}
}
