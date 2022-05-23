using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
	public OrderType[] targetOrders; //orders where this object is the target
	private void Awake()
	{
		MouseInteractionTracker.mouseHoveredOnInteractable += OnHover;
		MouseInteractionTracker.mouseHoveredOffInteractable += OffHover;
	}

	protected virtual void OffHover(InteractableObject obj)
	{
		if (obj == this)
		{
			this.transform.localScale /= 0.9f;
		}
	}

	protected virtual void OnHover(InteractableObject obj)
	{
		if (obj == this)
		{
			this.transform.localScale *= 0.9f;
		}
	}
}
