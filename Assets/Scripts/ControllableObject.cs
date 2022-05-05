using System;
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

	internal Dictionary<string, Action<InteractableObject>> GetActionsFromBehaviours()
	{
		Dictionary<string, Action<InteractableObject>> actions = new Dictionary<string, Action<InteractableObject>>();
		CustomBehaviour[] customBehaviours = GetComponents<CustomBehaviour>();
		foreach (var item in customBehaviours)
		{
			foreach (var action in item.actions)
			{
				actions.Add(action.Key, action.Value);
			}
		}
		return actions;
	}
}
