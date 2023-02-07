using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGamePreview
{
	public event Action FinishedPreviewEvent;

	public void OnRightClickInteractable(InteractableObject interactableObject, Vector3 worldMousePosition);

	public void OnLeftClickInteractable(InteractableObject interactableObject, Vector3 worldMousePosition);

	public void UpdatePreview();

	public void DestroyPreview();
}
