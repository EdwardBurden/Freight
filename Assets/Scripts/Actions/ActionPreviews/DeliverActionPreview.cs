using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class DeliverActionPreview : ActionPreview
{
	private Action<IConsumer, Vector3> callback;

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
			callback.Invoke(consumer, pos);
			Close();
		}
	}

	public void Open(ShipObject shipObject, Action<IConsumer, Vector3> callback)
	{
		this.callback = callback;
		this.shipObject = shipObject;
		Open();
	}

}
