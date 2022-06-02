using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
	[SerializeField]
	private Material defaultMaterial;
	[SerializeField]
	private Material hoverMaterial;
	[SerializeField]
	private Material clickMaterial;
	[SerializeField]
	private MeshRenderer renderer;
	protected virtual void Awake()
	{
		MouseInteractionTracker.mouseHoveredOnInteractable += OnHover;
		MouseInteractionTracker.mouseHoveredOffInteractable += OffHover;
	}

	protected virtual void OffHover(InteractableObject obj)
	{
		if (obj == this && renderer != null)
		{

			renderer.material = defaultMaterial;
			//this.transform.localScale /= 0.9f;
		}
	}

	protected virtual void OnHover(InteractableObject obj)
	{
		if (obj == this && renderer != null)
		{
			renderer.material = hoverMaterial;
			//this.transform.localScale *= 0.9f;
		}
	}
}
