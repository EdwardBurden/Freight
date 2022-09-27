using System;
using UnityEngine;

public abstract class ActionPreview : MonoBehaviour
{
	public event Action FinishedPreviewEvent;

	[SerializeField]
	protected Vector3 offset = new Vector3(0, 3, 0);

	[SerializeField]
	protected LineRenderer lineRenderer;

	protected Ship shipObject;
	protected Vector3 target;

	private void Awake()
	{
		lineRenderer.gameObject.SetActive(true);
	}

	public abstract void OnRightClickInteractable(InteractableObject interactableObject, Vector3 worldMousePosition);

	public abstract void OnLeftClickInteractable(InteractableObject interactableObject, Vector3 worldMousePosition);

	public void Init(Ship shipObject)
	{
		this.shipObject = shipObject;
	}

	public void UpdatePreview()
	{
		target = MouseInput.worldMousePosition;
		lineRenderer.SetPosition(0, shipObject.gameObject.transform.position + offset);
		lineRenderer.SetPosition(1, target);
	}

	public void RaiseFinishedEvent()
	{
		FinishedPreviewEvent?.Invoke();
	}
}
