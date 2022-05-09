using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionOrder : Order
{
	protected InteractableObject interactableObject;
	public InteractionOrder(Orderable orderable, InteractableObject interactableObject) : base(orderable)
	{
		this.interactableObject = interactableObject;
	}
}
