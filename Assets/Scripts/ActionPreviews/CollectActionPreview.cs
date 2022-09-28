using System;
using UI;
using UnityEngine;

public class CollectActionPreview : ActionPreview
{
	public Action<IProducer, ResourceDefintion> callback;

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
		IProducer producer = interactableObject.GetComponent<IProducer>();
		if (producer != null)
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
		IProducer producer = interactableObject.GetComponent<IProducer>();
		if (producer != null)
		{
			ResourceSelectScreen.Create(producer.GetProducedResources(), pos, (resource) => OnResourceSelected(producer, resource), true);
		}
	}

	private void OnResourceSelected(IProducer producer, ResourceDefintion resourceDefintion)
	{
		RaiseFinishedEvent();
		callback?.Invoke(producer, resourceDefintion);
	}
}