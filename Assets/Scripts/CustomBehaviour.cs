using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class CustomBehaviour : MonoBehaviour //make abstract
{
	public Dictionary<string, Action<InteractableObject>> actions = new Dictionary<string, Action<InteractableObject>>();
	//draw list of string- method, where user can create a new action with name and method assigned

	private void Awake()
	{
		actions.Add("Test", (InteractableObject) => move()); //stuff this object can do
	}

	private void move()
	{
		Debug.LogError("test");
	}



	//For most behaviour, the object controlled will need to know the interaction object, then for other paras can be set on awake like speed, when complete the behaviour need to call out to tell the world

}
