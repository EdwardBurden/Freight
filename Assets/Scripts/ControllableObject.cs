using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
[RequireComponent(typeof(Orderable))]
public class ControllableObject : InteractableObject //controlled by player
{
	[SerializeField]
	//private ShipDefintion shipDefintion; // holds info and orders defintions for now, can 
	public Orderable orderable => GetComponent<Orderable>();
	//private ShipDefintion  //can be more abstract later 
	//BASICALLY A SHIP, SOMETHING THAT HAS ACTION WHEN INTERACTING WITH ANOTHER THING

	/*public List<InteractionOrderDefintion> GetAvailableInteractionOrders(InteractableObject interactableObject, Vector3 postion)
	{
		List<InteractionOrderDefintion> interactionOrderDefintions = new List<InteractionOrderDefintion>();
		foreach (InteractionOrderDefintion interactionOrder in shipDefintion.orderDefintions)
		{
			if (interactionOrder.CanCreateInteractionOrder(this, interactableObject, postion))
			{
				interactionOrderDefintions.Add(interactionOrder);
			}
		}
		return interactionOrderDefintions;
	}*/



}
