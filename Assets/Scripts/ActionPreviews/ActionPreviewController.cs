using System;
using UnityEngine;

public class ActionPreviewController : Singleton<ActionPreviewController>
{
	public static event Action PreviewComplete;

	private ActionPreview currentPreview;

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

	public ActionPreview CreateActionPreview(Ship ship, ActionPreview actionPreviewPrefab)
	{
		DestroyCurrentPreview();
		currentPreview = Instantiate(actionPreviewPrefab, this.transform);
		currentPreview.Init(ship);
		currentPreview.FinishedPreviewEvent += FinishActionPreview;
		GameSceneController.instance.SetInputToPreviewMode();
		//UIManager.instance.CloseAllScreens();
		return currentPreview;
	}

	public void FinishActionPreview()
	{
		currentPreview.FinishedPreviewEvent -= FinishActionPreview;
		GameSceneController.instance.SetInputToSelectionMode();
		//UIManager.instance.CloseAllScreens();
		DestroyCurrentPreview();
	}

	public void DestroyCurrentPreview()
	{
		if (currentPreview != null)
		{
			Destroy(currentPreview.gameObject);
		}
	}
}
