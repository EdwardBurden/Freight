using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllableObject : InteractableObject
{
	//private ShipDefintion  //can be more abstract later 
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

	public string[] GetInteractionActionNames() //assume controllable is not nul and target are not null
	{
		CustomBehaviour[] behaviours = GetCustomBehaviours();
		string[] names = new string[behaviours.Length];
		for (int i = 0; i < behaviours.Length; i++)
		{
			names[i] = behaviours[i].actionName;
		}
		return names;
	}


	public CustomBehaviour[] GetCustomBehaviours()
	{
		CustomBehaviour[] customBehaviours = GetComponents<CustomBehaviour>();
		return customBehaviours;
	}
}
