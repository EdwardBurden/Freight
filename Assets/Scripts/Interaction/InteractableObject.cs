using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public abstract class InteractableObject : MonoBehaviour
{
	protected virtual void Awake()
	{
		MouseInteractionTracker.mouseHoveredOnInteractable += OnHover; //temp can be removed or improved, since piced up by nearly everything
		MouseInteractionTracker.mouseHoveredOffInteractable += OffHover; //temp can be removed or improved, since piced up by nearly everything
	}

	protected virtual void OffHover(InteractableObject obj)
	{
		if (obj == this)
		{


			//this.transform.localScale /= 0.9f;
		}
	}

	protected virtual void OnHover(InteractableObject obj)
	{
		if (obj == this)
		{

			//this.transform.localScale *= 0.9f;
		}
	}
}
