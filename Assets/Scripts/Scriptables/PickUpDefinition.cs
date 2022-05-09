using Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/PickUpDefinition")]
public class PickUpDefinition : InteractionOrderDefintion
{
	public override bool CanCreateInteractionOrder(ControllableObject controllableObject, InteractableObject target)
	{
		return controllableObject.GetComponent<MoveBehaviour>() != null && controllableObject.GetComponent<PickUpBehaviour>() != null; ;
		//if target has pickup allowed?, or pickable component
	}

	public override InteractionOrder CreateInteractionOrder(ControllableObject controllableObject, InteractableObject target)
	{
		return new PickUpOrder(target, controllableObject.orderable);
	}
}
