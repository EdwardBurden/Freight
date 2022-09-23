using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MouseInput : MonoBehaviour
{
	[SerializeField]
	private Camera mainCamera;

	private GameControls inputActions;
	private InteractableObject localHoverObject;


	public static Vector3 mousePostion;

	public static event Action<InteractableObject> mouseHoveredOnInteractable;
	public static event Action<InteractableObject> mouseHoveredOffInteractable;

	public static event Action LeftClickNonInteractableEvent;
	public static event Action RightClickNonInteractableEvent;
	public static event Action<InteractableObject, Vector3> LeftClickInteractableEvent;
	public static event Action<InteractableObject, Vector3> RightClickInteractableEvent;


	private void Awake()
	{
		inputActions = new GameControls();
	}

	private void OnEnable()
	{
		inputActions.Interactions.Enable();
	}

	private void OnDisable()
	{
		inputActions.Interactions.Disable();
	}

	private void Update()
	{
		bool onUI = RayCastUtils.CheckIfObjectIsUIInFocus(Mouse.current.position.ReadValue());
		RayCastInfo<InteractableObject> interactionInfo = RayCastUtils.CheckIfObjectIsInFocus<InteractableObject>(mainCamera);
		if (onUI) //maybe include this bool in the events rsather than block
		{
			return;
		}

		CheckHoverObject(interactionInfo);
		CheckClick(interactionInfo);
		if (interactionInfo.focused)
		{
			mousePostion = interactionInfo.hitInfo.point ;
		}
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
		if (inputActions.Interactions.Select.WasPerformedThisFrame())
		{
			OnLeftClickObject(rayCastInfo);
		}
		if (inputActions.Interactions.ImmediateOrder.WasPerformedThisFrame())
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
			LeftClickInteractableEvent?.Invoke(rayCastInfo.focusObject, rayCastInfo.hitInfo.point);
		}
		else
		{
			LeftClickNonInteractableEvent?.Invoke();
		}
	}

	private void OnRightClickObject(RayCastInfo<InteractableObject> rayCastInfo)
	{
		if (rayCastInfo.focused)
		{
			RightClickInteractableEvent?.Invoke(rayCastInfo.focusObject, rayCastInfo.hitInfo.point);
		}
		else
		{
			RightClickNonInteractableEvent?.Invoke();
		}
	}
}