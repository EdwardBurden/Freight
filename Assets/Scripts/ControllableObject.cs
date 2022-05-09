using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
[RequireComponent(typeof(Orderable))]
public class ControllableObject : InteractableObject //controlled by player
{
	public Orderable orderable => GetComponent<Orderable>();
	//private ShipDefintion  //can be more abstract later 
	//BASICALLY A SHIP, SOMETHING THAT HAS ACTION WHEN INTERACTING WITH ANOTHER THING





}
