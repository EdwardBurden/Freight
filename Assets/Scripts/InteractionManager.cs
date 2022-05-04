using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
	public ControllableObject controlledObject;  //object we have selected
	public InteractableObject targetInteractableObject; //object we are hovering above 


	private void Awake()
	{
		MouseInteractionTracker.mouseHoveredOnInteractable += MouseInteractionTracker_mouseHoveredOnInteractable;
		MouseInteractionTracker.mouseHoveredOffInteractable += MouseInteractionTracker_mouseHoveredOffInteractable;
	}

	private void MouseInteractionTracker_mouseHoveredOffInteractable(InteractableObject obj)
	{
		Debug.Log("Off " + obj.name);
	}

	private void MouseInteractionTracker_mouseHoveredOnInteractable(InteractableObject obj)
	{
		Debug.Log("On " + obj.name);
	}

	private void Update()
	{

	}

	//User Starts with ship selected
	//when mouse hovers above other objects highlight them if they have the InteractableObject component

	public void OpenInteractionOptions()
	{
		//With object selected, user right clicks 
		//game displays list of possible interactions
		//interacts possible are based on controlled object
		//and object thats selected
		//some targets can have uniuqe interactions

		//while user selects ther option lock targetInteractableObject to not change so they dont hver over another object while choosing
		//targetInteractableObject is unlocked when user selects or exits
	}

	public void SelectControllableObject()
	{
		//remove old controllable
		//set new one

	}

}
