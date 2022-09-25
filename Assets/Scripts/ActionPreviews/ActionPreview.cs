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

	public event Action FinishedPreviewEvent;

	private void Awake()
	{
		lineRenderer.gameObject.SetActive(true);
	}

	public abstract void OnRightClickInteractable(InteractableObject arg1, Vector3 arg2);


	public abstract void OnLeftClickInteractable(InteractableObject arg1, Vector3 arg2);

	public void Init(Ship shipObject)
	{
		this.shipObject = shipObject;
	}

	public void UpdatePreview()
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

	public void RaiseFinsihedEvent()
	{
		FinishedPreviewEvent?.Invoke();

	}
}
