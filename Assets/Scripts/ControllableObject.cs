using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllableObject : InteractableObject
{
	//BASICALLY A SHIP, SOMETHING THAT HAS ACTION WHEN INTERACTING WITH ANOTHER THING

	private void Awake()
	{
		MouseInteractionTracker.mouseHoveredOnInteractable += MouseInteractionTracker_mouseHoveredOnInteractable; //FORTESTING
		MouseInteractionTracker.mouseHoveredOffInteractable += MouseInteractionTracker_mouseHoveredOffInteractable;//FORTESTING
	}

	private void MouseInteractionTracker_mouseHoveredOffInteractable(InteractableObject obj)
	{
		if (obj == this)
		{
			this.transform.GetChild(0).localScale /= 0.5f;
		}
	}

	private void MouseInteractionTracker_mouseHoveredOnInteractable(InteractableObject obj)
	{
		if (obj == this)
		{
			this.transform.GetChild(0).localScale *= 0.5f;
		}
	}
}
