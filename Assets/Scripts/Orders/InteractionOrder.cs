using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionOrder : Order
{
	protected InteractableObject interactableObject;
	protected Vector3 targetPosition;

	public InteractionOrder(Orderable orderable, InteractableObject interactableObject ,Vector3 clickPosition) : base(orderable)
	{
		this.interactableObject = interactableObject;
		targetPosition = clickPosition;
	}
}
