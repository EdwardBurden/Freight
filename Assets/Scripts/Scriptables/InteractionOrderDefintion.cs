using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionOrderDefintion : OrderDefinition<InteractionOrder>
{
	public abstract bool CanCreateInteractionOrder(ControllableObject controllableObject, InteractableObject target, Vector3 postion);
	public abstract InteractionOrder CreateInteractionOrder(ControllableObject controllableObject, InteractableObject target, Vector3 postion);
}
