using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{

	public ControllableObject controlledObject;  //object we have selected
	public InteractableObject targetInteractableObject; //object we are hovering above 
	private bool inRightClickContext;


	private void Awake()
	{
		inRightClickContext = false;
		MouseInteractionTracker.mouseHoveredOnInteractable += OnHoverOnInteractable;
		MouseInteractionTracker.mouseHoveredOffInteractable += OnHoverOffInteractable;

		MouseInteractionTracker.mouseLeftClickedInteractable += OnLeftClickInteractable;
		MouseInteractionTracker.mouseLeftClickedNothing += OnLeftClickNotInteractable;

		MouseInteractionTracker.mouseRightClickedInteractable += OnRightClickInteractable;
	}

	private void OnLeftClickNotInteractable()
	{
		if (inRightClickContext)
		{
			CloseInteractionOptions();
		}
	}

	private void OnRightClickInteractable(InteractableObject obj)
	{
		if (obj == targetInteractableObject && obj != controlledObject)
		{
			OpenInteractionOptions();
		}
	}

	private void OnLeftClickInteractable(InteractableObject obj)
	{
		if (inRightClickContext)
		{
			CloseInteractionOptions();
		}
		if (obj is ControllableObject controllableObject && controllableObject != controlledObject)
		{
			SelectControllableObject(controllableObject);
		}
	}

	private void OnHoverOffInteractable(InteractableObject obj)
	{
		if (inRightClickContext)
		{
			return;
		}
		if (obj == targetInteractableObject)
		{
			targetInteractableObject = null;
			Debug.Log("Off " + obj.name);
		}
	}

	private void OnHoverOnInteractable(InteractableObject obj)
	{
		if (inRightClickContext)
		{
			return;
		}
		targetInteractableObject = obj;
		Debug.Log("On " + obj.name);
	}

	private void OpenInteractionOptions()
	{
		inRightClickContext = true;
		//GO TO UI
		Debug.Log("OpenInteractionOptions");
	}

	private void CloseInteractionOptions()
	{
		inRightClickContext = false;
		//GO TO UI
		Debug.Log("CloseInteractionOptions");
	}

	private void SelectControllableObject(ControllableObject controllableObject)
	{
		//controlledObject.ControlLost() //maybe event we'll see
		controlledObject = null;
		controlledObject = controllableObject;
		Debug.Log("New Controllable " + controllableObject.name);
	}

}
