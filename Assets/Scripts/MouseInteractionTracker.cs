using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static InteractionUtils;

public class MouseInteractionTracker : MonoBehaviour //find better name, it keeps track of where the players mouse is and when it clicks an object it tell the interaction manager
{
	//make similai movement class for default movement , outside of orders
	private InteractableObject localHoverObject;

	//MAKE STATIC

	public static event Action<InteractableObject> mouseHoveredOnInteractable;
	public static event Action<InteractableObject> mouseHoveredOffInteractable;

	public static event Action mouseLeftClickedNothing;
	public static event Action<InteractableObject> mouseLeftClickedInteractable;
	public static event Action<InteractableObject, Vector3> mouseRightClickedInteractable;

	//DOESNT NEED TO KNOW WHAT THE MANAGER IS DOING, DOESNT TAKE ORDERS FROM THE MANAGER, DOESNT TELL THE MANAGER ANYTHING
	//SENDS OFF EVENTS WITH OBJECTS WHEN IT HOVERS OVER OR OFF THINGS 
	//SENDS OFF EVENTS WHEN USER CLICKS ON AN INTERACTABLE OBJECT
	//SENDS OFF EVENTS WHEN USER RIGHT CLICKS ON AN INTERACTABLE OBJECT

	private void Update()
	{
		if (EventSystem.current.IsPointerOverGameObject())
		{
			return;
		}
		RayCastInfo<InteractableObject> interactionInfo = InteractionUtils.CheckIfObjectIsInFocus<InteractableObject>();
		CheckHoverObject(interactionInfo);
		CheckClick(interactionInfo);
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


	private void CheckClick(RayCastInfo<InteractableObject> rayCastInfo) //for now only handle clicking on one object
	{
		//https://github.com/EdwardBurden/FUBAR/blob/master/Assets/Assets/Scripts/Battle/Selection/SelectionController.cs
		if (Input.GetMouseButtonDown(0))
		{
			OnLeftClick(rayCastInfo);
		}
		if (Input.GetMouseButtonDown(1))
		{
			OnRightClick(rayCastInfo);
		}
	}

	private void OnLeftClick(RayCastInfo<InteractableObject> rayCastInfo)
	{
		if (rayCastInfo.focused)
		{
			mouseLeftClickedInteractable?.Invoke(rayCastInfo.focusObject);
		}
		else
		{
			mouseLeftClickedNothing?.Invoke();
		}
	}

	private void OnRightClick(RayCastInfo<InteractableObject> rayCastInfo)
	{
		if (rayCastInfo.focused)
		{
			mouseRightClickedInteractable?.Invoke(rayCastInfo.focusObject, rayCastInfo.hitInfo.point);
		}
		else
		{
			mouseLeftClickedNothing?.Invoke();
		}
	}
}
