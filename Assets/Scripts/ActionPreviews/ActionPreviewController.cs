using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPreviewController : Singleton<ActionPreviewController>
{
	public static event Action preivewComplete;

	private ActionPreview currentActionPreview;

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

	private void OnRightClickInteractable(InteractableObject arg1, Vector3 arg2)
	{
		currentActionPreview?.OnRightClickInteractable(arg1, arg2);
	}

	private void OnLeftClickInteractable(InteractableObject arg1, Vector3 arg2)
	{
		currentActionPreview?.OnLeftClickInteractable(arg1, arg2);
	}

	public void SetActionPreview(ActionPreview actionPreview)
	{
		RemoveActionPreview();
		currentActionPreview = actionPreview;
		currentActionPreview.FinishedPreviewEvent += FinishActionPreview;
		currentActionPreview.transform.parent = this.transform;
		GameManager.instance.SetInputToPreviewMode();
		UIManager.instance.CloseAllScreens();
	}

	public void FinishActionPreview()
	{
		currentActionPreview.FinishedPreviewEvent -= FinishActionPreview;
		GameManager.instance.SetInputToSelectionMode();
		UIManager.instance.CloseAllScreens();
		RemoveActionPreview();
	}

	public void RemoveActionPreview()
	{
		if (currentActionPreview != null)
		{
			Destroy(currentActionPreview.gameObject);
		}
	}

	public void Update()
	{
		if (currentActionPreview != null)
		{
			currentActionPreview.UpdatePreview();
		}
	}
}
