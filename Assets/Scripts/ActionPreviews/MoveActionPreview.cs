using System;
using UnityEngine;

public class MoveActionPreview : ActionPreview
{
	public Action<Vector3> callback;

	public override void OnRightClickInteractable(InteractableObject interactableObject, Vector3 position)
	{
		RaiseFinishedEvent();
	}

	public override void OnLeftClickInteractable(InteractableObject interactableObject, Vector3 position)
	{
		RaiseFinishedEvent();
		callback?.Invoke(position);
	}

	public override void DestroyPreview()
	{
		Destroy(gameObject);
	}
}
