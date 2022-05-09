using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class InteractableObject : MonoBehaviour
{
	//COULD BE ANYTHING THAT HAS A BEHAVIOUR THAT CAN BE ACTED UPON

	private void Awake()
	{
	//	MouseInteractionTracker.mouseHoveredOnInteractable += MouseInteractionTracker_mouseHoveredOnInteractable; //FORTESTING
		//MouseInteractionTracker.mouseHoveredOffInteractable += MouseInteractionTracker_mouseHoveredOffInteractable;//FORTESTING
	}

	private void MouseInteractionTracker_mouseHoveredOffInteractable(InteractableObject obj)
	{
		if (obj == this)
		{
			this.transform.localScale /= 0.9f;
		}
	}

	private void MouseInteractionTracker_mouseHoveredOnInteractable(InteractableObject obj)
	{
		if (obj == this)
		{
			this.transform.localScale *= 0.9f;
		}
	}
}
