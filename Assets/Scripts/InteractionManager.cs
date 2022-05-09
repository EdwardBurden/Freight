using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
	public InteractionContextMenu interactionContextMenu; //placed on ui canvas for now
	private ControllableObject controlledObject;  //object we have selected
	private InteractableObject targetInteractableObject; //object right clicked

	private void Awake()
	{
		RegisterEvents();
	}


	private void RegisterEvents()
	{
		MouseInteractionTracker.mouseHoveredOnInteractable += OnHoverOnInteractable;
		MouseInteractionTracker.mouseHoveredOffInteractable += OnHoverOffInteractable;
		MouseInteractionTracker.mouseLeftClickedInteractable += OnLeftClickInteractable;
		MouseInteractionTracker.mouseLeftClickedNothing += OnLeftClickNotInteractable;
		MouseInteractionTracker.mouseRightClickedInteractable += OnRightClickInteractable;
	}

	private void OnLeftClickNotInteractable()
	{

	}

	private void OnRightClickInteractable(InteractableObject obj, Vector3 worldClickPosition)
	{
		if (obj != controlledObject)
		{
			targetInteractableObject = obj;
			OpenInteractionOptions(worldClickPosition);

		}
	}

	private void OnLeftClickInteractable(InteractableObject obj)
	{
		if (obj is ControllableObject controllableObject && controllableObject != controlledObject)
		{
			SelectControllableObject(controllableObject);
		}
	}

	private void OnHoverOffInteractable(InteractableObject obj)
	{
		if (interactionContextMenu.IsMenuOpen())
		{
			return;
		}

		//targetInteractableObject = null;
		Debug.Log("Off " + obj.name);

	}

	private void OnHoverOnInteractable(InteractableObject obj)
	{
		if (interactionContextMenu.IsMenuOpen())
		{
			return;
		}
		//targetInteractableObject = obj;
		Debug.Log("On " + obj.name);
	}

	private void OpenInteractionOptions(Vector3 worldHitPosition)
	{
		//GO TO UI
		Debug.Log("OpenInteractionOptions");
		if (true) //debug testing, remove  
		{
			Move(worldHitPosition);
			//pickUp();
			return;
		}
		/*string[] actionNames = controlledObject.GetInteractionActionNames(); //GET FROM DEFINTION(LIST OF AVIALBLE ORDERS)
		interactionContextMenu.Open(worldHitPosition);
		interactionContextMenu.DisplayOptions(actionNames);
		*/
	}

	private void SelectControllableObject(ControllableObject controllableObject)
	{
		//controlledObject.ControlLost() //maybe event we'll see
		controlledObject = null;
		controlledObject = controllableObject;
		Debug.Log("New Controllable " + controllableObject.name);
	}

	private void PutDown() //MOVE OUT WHEN DONE PROTOING
	{
		
	}

	private void PickUp() //MOVE OUT WHEN DONE PROTOING
	{
		PickUpOrder pickup = new PickUpOrder(targetInteractableObject, controlledObject.orderable);
		controlledObject.orderable.AddOrder(pickup);
		controlledObject.orderable.StartOrders();
	}

	private void Move(Vector3 position) //MOVE OUT WHEN DONE PROTOING
	{
		MoveOrder moveOrder = new MoveOrder(position, controlledObject.orderable);
		controlledObject.orderable.AddOrder(moveOrder);
		controlledObject.orderable.StartOrders();
	}
}
