using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRewardPreview : MonoBehaviour, IGamePreview
{
	public event Action FinishedPreviewEvent;

	public void DestroyPreview()
	{
		throw new NotImplementedException();
	}

	public void OnLeftClickInteractable(InteractableObject interactableObject, Vector3 worldMousePosition)
	{
		throw new NotImplementedException();
	}

	public void OnRightClickInteractable(InteractableObject interactableObject, Vector3 worldMousePosition)
	{
		throw new NotImplementedException();
	}

	public void UpdatePreview()
	{
		throw new NotImplementedException();
	}
}
