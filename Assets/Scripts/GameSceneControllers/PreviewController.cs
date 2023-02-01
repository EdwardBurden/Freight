using System;
using UI;
using UnityEngine;

public class PreviewController : Singleton<PreviewController>, IGameController
{
	public static event Action PreviewComplete;

	private IGamePreview currentPreview;

	private void Awake()
	{
		_instance = this;
	}

	private void OnEnable()
	{
		MouseInput.LeftClickInteractableEvent += OnLeftClickInteractable;
		MouseInput.RightClickInteractableEvent += OnRightClickInteractable;
	}

	private void OnDisable()
	{
		MouseInput.LeftClickInteractableEvent -= OnLeftClickInteractable;
		MouseInput.RightClickInteractableEvent -= OnRightClickInteractable;
	}

	public void Update()
	{
		if (currentPreview != null)
		{
			currentPreview.UpdatePreview();
		}
	}

	private void OnRightClickInteractable(InteractableObject interactableObject, Vector3 worldMousePosition)
	{
		currentPreview?.OnRightClickInteractable(interactableObject, worldMousePosition);
	}

	private void OnLeftClickInteractable(InteractableObject interactableObject, Vector3 worldMousePosition)
	{
		currentPreview?.OnLeftClickInteractable(interactableObject, worldMousePosition);
	}

	public void SetPreview(IGamePreview gamePreview)
	{
		DestroyCurrentPreview();
		currentPreview = gamePreview;
		currentPreview.FinishedPreviewEvent += FinishActionPreview;
		GameManager.instance.SetInputToPreviewMode();
		GameUIController.instance.CloseAllScreens();
	}

	public void FinishActionPreview()
	{
		currentPreview.FinishedPreviewEvent -= FinishActionPreview;	
		DestroyCurrentPreview();
		GameManager.instance.SetInputToSelectionMode();
		GameUIController.instance.CloseAllScreens();
	}

	public void DestroyCurrentPreview()
	{
		if (currentPreview != null)
		{
			currentPreview.DestroyPreview();
		}
		currentPreview = null;
	}
}
