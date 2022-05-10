using Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/PickUpDefinition")]
public class PickUpDefinition : InteractionOrderDefintion
{
	public override bool CanCreateInteractionOrder(ControllableObject controllableObject, InteractableObject target, Vector3 postion)
	{
		return controllableObject.GetComponent<MoveBehaviour>() != null && controllableObject.GetComponent<CarryBehaviour>() != null && target.GetComponent<Carryable>();
	}

	public override InteractionOrder CreateInteractionOrder(ControllableObject controllableObject, InteractableObject target, Vector3 postion)
	{
		return new PickUpOrder(target, controllableObject.orderable, postion);
	}
}
