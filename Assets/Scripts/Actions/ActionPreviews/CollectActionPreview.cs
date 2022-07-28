using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class CollectActionPreview : ActionPreview
{
	private Action<IProducer, ResourceDefintion> callback;

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
	public override void OnDestroy()
	{
		base.OnDestroy();
		MouseInteractionTracker.instance.mouseHoveredOnInteractable -= OnMouseHoveredOnInteractable;
		MouseInteractionTracker.instance.mouseHoveredOffInteractable -= OnMouseHoveredOffInteractable;
	}

	private void OnMouseHoveredOffInteractable(InteractableObject interactableObject)
	{
		if (!updateLine)
		{
			return;
		}
		lineRenderer.startColor = Color.red;
		lineRenderer.endColor = Color.red;
	}

	private void OnMouseHoveredOnInteractable(InteractableObject interactableObject)
	{
		if (!updateLine)
		{
			return;
		}
		IProducer producer = interactableObject.GetComponent<IProducer>(); //replace with chekcing the defintion can create 
		if (producer != null)
		{
			lineRenderer.startColor = Color.green;
			lineRenderer.endColor = Color.green;
		}
	}



	protected override void OnRightClickInteractable(InteractableObject interactableObject, Vector3 pos)
	{
		Close(); //and close resource
	}

	protected override void OnLeftClickInteractable(InteractableObject interactableObject, Vector3 pos)
	{
		IProducer producer = interactableObject.GetComponent<IProducer>();
		if (producer != null)
		{
			OnPreviewClick(producer, pos);
			//Close();
		}
	}
	public void Open(ShipObject shipObject, Action<IProducer, ResourceDefintion> callback)
	{
		this.callback = callback;
		this.shipObject = shipObject;
		Open();
	}

	private void OnPreviewClick(IProducer producer, Vector3 clickPosition)
	{
		updateLine = false;
		ResourceSelectScreen.Create(producer.GetProducedResources(), clickPosition, (resource) => OnResourceSelected(producer, resource), true);
	}

	private void OnResourceSelected(IProducer producer, ResourceDefintion resourceDefintion)
	{
		callback?.Invoke(producer, resourceDefintion);
		Close();
	}
}