using Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/MoveDefinition")]
public class MoveDefinition : InteractionOrderDefintion
{
	public float speed = 50;

	public override bool CanCreateInteractionOrder(ControllableObject controllableObject, InteractableObject target, Vector3 postion)
	{
		return controllableObject.GetComponent<MoveBehaviour>() != null;
	}

	public override InteractionOrder CreateInteractionOrder(ControllableObject controllableObject, InteractableObject target, Vector3 postion)
	{
		MoveOrder moveOrder = new MoveOrder(target, controllableObject.orderable, postion);
		return moveOrder;
	}

}
