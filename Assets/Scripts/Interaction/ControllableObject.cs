using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
[RequireComponent(typeof(Orderable))]
public class ControllableObject : InteractableObject
{
	public GameActionDefintion[] actionDefintions;
	public Orderable orderable => GetComponent<Orderable>();


}
