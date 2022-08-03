using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class DeliverActionPreview : ActionPreview
{
	private Action<IConsumer, ResourceDefintion> callback;

	private void Awake()
	{
		lineRenderer.gameObject.SetActive(false);
	}
	public override void Start()
	{
		base.Start();
		MouseInteractionTracker.instance.mouseHoveredOnInteractable += OnMouseHoveredOnInteractable;
		MouseInteractionTracker.instance.mouseHoveredOffInteractable += OnMouseHoveredOffInteractable;
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
		IConsumer consumer = interactableObject.GetComponent<IConsumer>(); //replace with chekcing the defintion can create 
		if (consumer != null)
		{
			lineRenderer.SetPosition(0, shipObject.gameObject.transform.position + offset);
			lineRenderer.SetPosition(1, interactableObject.transform.position);
			lineRenderer.startColor = Color.green;
			lineRenderer.endColor = Color.green;
		}
	}

	public override void OnDestroy()
	{
		base.OnDestroy();
		MouseInteractionTracker.instance.mouseHoveredOnInteractable -= OnMouseHoveredOnInteractable;
		MouseInteractionTracker.instance.mouseHoveredOffInteractable -= OnMouseHoveredOffInteractable;
	}

	protected override void OnRightClickInteractable(InteractableObject interactableObject, Vector3 pos)
	{
		Close();
	}

	protected override void OnLeftClickInteractable(InteractableObject interactableObject, Vector3 pos)
	{
		IConsumer consumer = interactableObject.GetComponent<IConsumer>();
		if (consumer != null)
		{
			OnPreviewClick(consumer, pos);
		//	Close();
		}
	}

	public void Open(Ship shipObject, Action<IConsumer, ResourceDefintion> callback)
	{
		this.callback = callback;
		this.shipObject = shipObject;
		Open();
	}

	private void OnPreviewClick(IConsumer consumer, Vector3 clickPosition)
	{
		//pause the linerenderer, listen to event for resourceselecter closing?
		ResourceSelectScreen.Create(consumer.GetAcceptedResources(), clickPosition, (resource) => OnResourceSelected(consumer, resource), false);
	}

	private void OnResourceSelected(IConsumer consumer, ResourceDefintion resourceDefintion)
	{
		callback?.Invoke(consumer, resourceDefintion);
		Close();
	}
}
