using Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/MoveDefinition")]
public class MoveDefinition : OrderDefinition<MoveOrder>
{
	public float speed = 50;
	public bool CanCreateMoveDefintion(ControllableObject controllableObject, Vector3 target)
	{
		return controllableObject.GetComponent<MoveBehaviour>() != null; //and moveisnt disabled for example //&& area is navigateable
	}

	public MoveOrder CreateMoveDefintion(ControllableObject controllableObject, Vector3 target)
	{
		MoveOrder moveOrder = new MoveOrder(target, controllableObject.orderable);
		return moveOrder;
	}
}
