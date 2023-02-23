using System;
using UI;
using UnityEngine;

public class DeliverActionPreview : ActionPreview
{
	public Action<IConsumer, ResourceDefintion> callback;

	public void Start()
	{
		MouseInput.mouseHoveredOnInteractable += OnMouseHoveredOnInteractable;
		MouseInput.mouseHoveredOffInteractable += OnMouseHoveredOffInteractable;
	}

	public void OnDestroy()
	{
		MouseInput.mouseHoveredOnInteractable -= OnMouseHoveredOnInteractable;
		MouseInput.mouseHoveredOffInteractable -= OnMouseHoveredOffInteractable;
	}

	private void OnMouseHoveredOffInteractable(InteractableObject interactableObject)
	{
		lineRenderer.startColor = Color.red;
		lineRenderer.endColor = Color.red;
		lineRenderer.SetPosition(0, shipObject.gameObject.transform.position + offset);
		lineRenderer.SetPosition(1, shipObject.gameObject.transform.position + offset);
	}

	private void OnMouseHoveredOnInteractable(InteractableObject interactableObject)
	{
		IConsumer consumer = interactableObject.GetComponent<IConsumer>();
		if (consumer != null)
		{
			lineRenderer.SetPosition(0, shipObject.gameObject.transform.position + offset);
			lineRenderer.SetPosition(1, interactableObject.transform.position);
			lineRenderer.startColor = Color.green;
			lineRenderer.endColor = Color.green;
		}
	}

	public override void OnRightClickInteractable(InteractableObject interactableObject, Vector3 pos)
	{
		RaiseFinishedEvent();
	}

	public override void OnLeftClickInteractable(InteractableObject interactableObject, Vector3 pos)
	{
		IConsumer consumer = interactableObject.GetComponent<IConsumer>();
		if (consumer != null)
		{
			ResourceSelectScreen.Create(consumer.GetConsumedResources(), pos, (resource) => OnResourceSelected(consumer, resource), true);
		}
	}

	private void OnResourceSelected(IConsumer consumer, ResourceDefintion resourceDefintion)
	{
		RaiseFinishedEvent();
		callback?.Invoke(consumer, resourceDefintion);
	}

	public override void DestroyPreview()
	{
		Destroy(gameObject);
	}
}
