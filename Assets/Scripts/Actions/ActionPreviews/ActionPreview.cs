using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public abstract class ActionPreview : MonoBehaviour
{
	[SerializeField]
	protected Vector3 offset = new Vector3(0, 3, 0);

	[SerializeField]
	protected LineRenderer lineRenderer;

	protected ShipObject shipObject;

	private void Awake()
	{
		lineRenderer.gameObject.SetActive(false);
	}

	public virtual void Start()
	{
		ShipActionsPanel.ShipActionsPanelSelectionOpened += Close;
		MouseInteractionTracker.instance.mousePostion += OnMousePostion;
		MouseInteractionTracker.instance.LeftClickInteractableEvent += OnLeftClickInteractable;
		MouseInteractionTracker.instance.RightClickInteractableEvent += OnRightClickInteractable;
	}

	protected abstract void OnRightClickInteractable(InteractableObject arg1, Vector3 arg2);


	protected abstract void OnLeftClickInteractable(InteractableObject arg1, Vector3 arg2);


	protected virtual void OnMousePostion(Vector3 postion)
	{
		lineRenderer.SetPosition(0, shipObject.gameObject.transform.position + offset);
		lineRenderer.SetPosition(1, postion);
	}

	protected virtual void Open()
	{
		lineRenderer.gameObject.SetActive(true);
		ShipManager.instance.UnregisterEvents();
		UIManager.instance.CloseAllScreens();
	}

	protected virtual void Close()
	{
		ShipManager.instance.RegisterEvents();
		lineRenderer.gameObject.SetActive(false);
		Destroy(this.gameObject);
	}

	public virtual void OnDestroy()
	{
		ShipActionsPanel.ShipActionsPanelSelectionOpened -= Close;
		MouseInteractionTracker.instance.mousePostion -= OnMousePostion;
		MouseInteractionTracker.instance.LeftClickInteractableEvent -= OnLeftClickInteractable;
		MouseInteractionTracker.instance.RightClickInteractableEvent -= OnRightClickInteractable;
	}
}
