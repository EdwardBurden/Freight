using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static InteractionUtils;

public class MouseInteractionTracker : MonoBehaviour
{
	private InteractableObject localHoverObject;

	public static event Action<InteractableObject> mouseHoveredOnInteractable;
	public static event Action<InteractableObject> mouseHoveredOffInteractable;

	public static event Action<InteractableObject, Vector3> mouseLeftClickedInteractable;
	public static event Action<InteractableObject, Vector3> mouseRightClickedInteractable;

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
				OffHoverObject();
			}
			return; // return if not focused or has removed focus
		}

		if (localHoverObject == null)
		{
			OnHoverObject(rayCastInfo.focusObject);
		}
		else if (localHoverObject != rayCastInfo.focusObject)
		{
			OffHoverObject();
			OnHoverObject(rayCastInfo.focusObject);
		}
	}

	private void CheckClick(RayCastInfo<InteractableObject> rayCastInfo) //for now only handle clicking on one object
	{
		//https://github.com/EdwardBurden/FUBAR/blob/master/Assets/Assets/Scripts/Battle/Selection/SelectionController.cs
		if (Input.GetMouseButtonDown(0))
		{
			OnLeftClickObject(rayCastInfo);
		}
		if (Input.GetMouseButtonDown(1))
		{
			OnRightClickObject(rayCastInfo);
		}
	}

	private void OnHoverObject(InteractableObject interactableObject)
	{
		localHoverObject = interactableObject;
		mouseHoveredOnInteractable?.Invoke(interactableObject);
		localHoverObject.OnHover();
	}

	private void OffHoverObject()
	{
		mouseHoveredOffInteractable?.Invoke(localHoverObject);
		localHoverObject.OffHover();
		localHoverObject = null;
	}

	private void OnLeftClickObject(RayCastInfo<InteractableObject> rayCastInfo)
	{
		if (rayCastInfo.focused)
		{
			mouseLeftClickedInteractable?.Invoke(rayCastInfo.focusObject, rayCastInfo.hitInfo.point);
			//rayCastInfo.focusObject.OnLeftClick(); have interaction manger trigger these, as selected/deselected
		}
		else
		{
		//	mouseLeftClickedNothing?.Invoke();
		}
	}

	private void OnRightClickObject(RayCastInfo<InteractableObject> rayCastInfo)
	{
		if (rayCastInfo.focused)
		{
			mouseRightClickedInteractable?.Invoke(rayCastInfo.focusObject, rayCastInfo.hitInfo.point);
		//	rayCastInfo.focusObject.OnRigthClick(); have interaction manger trigger these
		}
		else
		{
			//mouseRightClickedNothing?.Invoke();
		}
	}
}