using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InteractionUtils;

public class MouseInteractionTracker : MonoBehaviour //find better name, it keeps track of where the players mouse is and when it clicks an object it tell the interaction manager
{
	private InteractableObject localHoverObject;

	//MAKE STATIC

	public static event Action<InteractableObject> mouseHoveredOnInteractable;
	public static event Action<InteractableObject> mouseHoveredOffInteractable;

	//public event Action mouseLeftClickedInteractable;
	//public event Action mouseRightClickedInteractable;

	//DOESNT NEED TO KNOW WHAT THE MANAGER IS DOING, DOESNT TAKE ORDERS FROM THE MANAGER, DOESNT TELL THE MANAGER ANYTHING
	//SENDS OFF EVENTS WITH OBJECTS WHEN IT HOVERS OVER OR OFF THINGS 
	//SENDS OFF EVENTS WHEN USER CLICKS ON AN INTERACTABLE OBJECT
	//SENDS OFF EVENTS WHEN USER RIGHT CLICKS ON AN INTERACTABLE OBJECT

	private void Update()
	{
		RayCastInfo<InteractableObject> rayCastInfo = InteractionUtils.CheckIfObjectIsInFocus<InteractableObject>();
		CheckHoverObject(rayCastInfo);
	}

	private void CheckHoverObject(RayCastInfo<InteractableObject> rayCastInfo)
	{
		if (!rayCastInfo.focused)
		{
			if (localHoverObject != null)
			{
				HoverRemovedFromObject();
			}
			return; // return if not focused or has removed focus
		}

		if (localHoverObject == null)
		{
			HoverAddedToObject(rayCastInfo.focusObject);
		}
		else if (localHoverObject != rayCastInfo.focusObject)
		{
			HoverRemovedFromObject();
			HoverAddedToObject(rayCastInfo.focusObject);
		}
	}

	private void HoverAddedToObject(InteractableObject interactableObject)
	{
		localHoverObject = interactableObject;
		mouseHoveredOnInteractable?.Invoke(interactableObject);
	}

	private void HoverRemovedFromObject()
	{
		mouseHoveredOffInteractable?.Invoke(localHoverObject);
		localHoverObject = null;
	}


	private void CheckClick(int something)
	{

	}

	//send off new object under hover event with InteractableObject

	//send off new controllable hover event with ControllableObject
}
