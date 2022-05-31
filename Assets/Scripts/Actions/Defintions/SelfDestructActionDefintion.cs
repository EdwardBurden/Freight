using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SelfDestructActionDefintion", order = 1)]
public class SelfDestructActionDefintion : GameActionDefintion
{
	public override bool ActionAvailable(ControllableObject controllableObject, InteractableObject interactableObject, Vector3 clickPostion)
	{
		return true;
	}

	public override void OnActionSelected(ControllableObject controllableObject, InteractableObject interactableObject, Vector3 clickPostion)
	{
		SelfDestructAction selfDestructAction = new SelfDestructAction(5, controllableObject.orderable, this);
		controllableObject.orderable.AddOrder(selfDestructAction);
		controllableObject.orderable.StartOrders();
	}
}
