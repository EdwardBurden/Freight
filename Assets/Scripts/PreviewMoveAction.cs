using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class PreviewMoveAction : MonoBehaviour //or created in the scen froma prefab and killed when doen, can decide later
{
	[SerializeField]
	private Vector3 offset = new Vector3(0, 3, 0);

	[SerializeField]
	private LineRenderer lineRenderer;

	private ShipObject shipObject;
	private Action<Vector3> callback;
	private bool open;

	private void Awake()
	{
		lineRenderer.gameObject.SetActive(false);
	}

	public void Start()
	{
		ShipActionsPanel.ShipActionsPanelSelectionOpened += Close;
		MouseInteractionTracker.instance.mousePostion += OnMousePostion;
		MouseInteractionTracker.instance.LeftClickInteractableEvent += OnLeftClickInteractable;
		MouseInteractionTracker.instance.RightClickInteractableEvent += OnRightClickInteractable;
	}

	public void OnDestroy()
	{
		ShipActionsPanel.ShipActionsPanelSelectionOpened -= Close;
		MouseInteractionTracker.instance.mousePostion -= OnMousePostion;
		MouseInteractionTracker.instance.LeftClickInteractableEvent -= OnLeftClickInteractable;
		MouseInteractionTracker.instance.RightClickInteractableEvent -= OnRightClickInteractable;
	}

	private void OnMousePostion(Vector3 postion)
	{
		lineRenderer.SetPosition(0, shipObject.gameObject.transform.position + offset);
		lineRenderer.SetPosition(1, postion);
	}

	private void OnRightClickInteractable(InteractableObject arg1, Vector3 arg2)
	{
		Close();
	}

	private void OnLeftClickInteractable(InteractableObject arg1, Vector3 arg2)
	{
		callback.Invoke(arg2);
		Close();
	}

	public void Open(ShipObject shipObject, Action<Vector3> callback)
	{
		if (open)
		{
			Close();
		}

		this.callback = callback;
		this.shipObject = shipObject;
		open = true;
		lineRenderer.gameObject.SetActive(true);
		ShipSelectionManager.instance.UnregisterEvents();
		UIManager.instance.CloseAllScreens();
	}

	private void Close()
	{
		open = false;
		ShipSelectionManager.instance.RegisterEvents();
		lineRenderer.gameObject.SetActive(false);
		Destroy(this.gameObject);
	}
}
