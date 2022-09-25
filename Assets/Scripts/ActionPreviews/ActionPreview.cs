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

	protected Ship shipObject;

	protected bool updateLine = true;
	private Vector3 target;

	private void Awake()
	{
		lineRenderer.gameObject.SetActive(false);
	}

	public virtual void Start()
	{
		MouseInput.LeftClickInteractableEvent += OnLeftClickInteractable;
		MouseInput.RightClickInteractableEvent += OnRightClickInteractable;
	}

	protected abstract void OnRightClickInteractable(InteractableObject arg1, Vector3 arg2);


	protected abstract void OnLeftClickInteractable(InteractableObject arg1, Vector3 arg2);


	private void Update()
	{
		if (updateLine)
		{
			target = MouseInput.mousePostion;
		}
		lineRenderer.SetPosition(0, shipObject.gameObject.transform.position + offset);
		lineRenderer.SetPosition(1, target);
	}

	protected virtual void OnMousePostion(Vector3 postion)
	{
		if (updateLine)
		{
			target = postion;
		}
		lineRenderer.SetPosition(0, shipObject.gameObject.transform.position + offset);
		lineRenderer.SetPosition(1, target);
	}

	protected virtual void Open()
	{
		lineRenderer.gameObject.SetActive(true);
		FindObjectOfType<ShipSelector>().UnregisterEvents();
		UIManager.instance.CloseAllScreens();
	}

	public virtual void Close()
	{
		FindObjectOfType<ShipSelector>().RegisterEvents();
		lineRenderer.gameObject.SetActive(false);
		Destroy(this.gameObject);
		UIManager.instance.CloseAllScreens();
	}

	public virtual void OnDestroy()
	{
		MouseInput.LeftClickInteractableEvent -= OnLeftClickInteractable;
		MouseInput.RightClickInteractableEvent -= OnRightClickInteractable;
	}
}
