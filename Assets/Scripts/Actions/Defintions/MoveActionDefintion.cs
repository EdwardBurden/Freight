using Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/MoveActionDefintion", order = 1)]
public class MoveActionDefintion : ActionDefintion
{
	public override bool ActionAvailable(ControllableObject controllableObject, InteractableObject interactableObject, Vector3 clickPostion)
	{
		MoveBehaviour moveBehaviour = controllableObject.gameObject.GetComponent<MoveBehaviour>();
		if (moveBehaviour == null)
		{
			return false;
		}
		return true;
	}

	public override void OnActionSelected(ControllableObject controllableObject, InteractableObject interactableObject, Vector3 clickPostion)
	{
		MoveAction moveAction = new MoveAction(clickPostion, controllableObject.orderable, this);
		controllableObject.orderable.AddOrder(moveAction);
		controllableObject.orderable.StartOrders();
	}
}
