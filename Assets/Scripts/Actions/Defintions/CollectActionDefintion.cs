using Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CollectActionDefintion", order = 1)]
public class CollectActionDefintion : ActionDefintion
{
	public override bool ActionAvailable(ControllableObject controllableObject, InteractableObject interactableObject, Vector3 clickPostion)
	{
		Producer producer = interactableObject as Producer;
		if (producer == null)
		{
			return false;
		}
		SingleCarryBehaviour carryBehaviour = controllableObject.gameObject.GetComponent<SingleCarryBehaviour>();
		if (carryBehaviour == null)
		{
			return false;
		}
		MoveBehaviour moveBehaviour = controllableObject.gameObject.GetComponent<MoveBehaviour>();
		if (moveBehaviour == null)
		{
			return false;
		}
		return producer.HasFreigthSpawned();
	}

	public override void OnActionSelected(ControllableObject controllableObject, InteractableObject interactableObject, Vector3 clickPostion)
	{
		Producer producer = interactableObject as Producer;
		Freight freight = producer.GetSpawnedFreight();
		//open ui to select resource
		CollectAction collectAction = new CollectAction(freight, producer, controllableObject.orderable, this);
		controllableObject.orderable.AddOrder(collectAction);
		controllableObject.orderable.StartOrders();
	}
}
