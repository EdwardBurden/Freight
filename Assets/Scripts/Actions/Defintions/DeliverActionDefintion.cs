
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Actions/DeliverActionDefintion", order = 1)]
public class DeliverActionDefintion : GameActionDefintion
{
	public override bool ActionAvailable(ControllableObject controllableObject, InteractableObject interactableObject, Vector3 clickPostion)
	{
		Consumer consumer = interactableObject as Consumer;
		if (consumer == null)
		{
			return false;
		}
		DeliverBehaviour carryBehaviour = controllableObject.gameObject.GetComponent<DeliverBehaviour>();
		if (carryBehaviour == null)
		{
			return false;
		}
		MoveBehaviour moveBehaviour = controllableObject.gameObject.GetComponent<MoveBehaviour>();
		if (moveBehaviour == null)
		{
			return false;
		}
		return true;
	}

	public override void OnActionSelected(ControllableObject controllableObject, InteractableObject interactableObject, Vector3 clickPostion)
	{
		Consumer consumer = interactableObject as Consumer;

		DeliverAction deliver = new DeliverAction( consumer, controllableObject.orderable, this);
		controllableObject.orderable.AddOrder(deliver);
		controllableObject.orderable.StartOrders();
	}
}
