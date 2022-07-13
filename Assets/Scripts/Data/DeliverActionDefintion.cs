using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TODO
[CreateAssetMenu(menuName = "ScriptableObject/DeliverActionDefintion")]
public class DeliverActionDefintion : ShipActionDefintion
{
	public override bool CanShowOption(ShipObject shipObject, InteractableObject interactableObject, Vector3 clickPostion)
	{
		IConsumer consumer = interactableObject.GetComponent<IConsumer>();
		CarryObject carryBehaviour = shipObject.gameObject.GetComponent<CarryObject>();
		if (consumer == null || carryBehaviour == null)
		{
			return false;
		}
		return true;
	}



	public override void OnOptionSelected(ShipObject shipObject, InteractableObject interactableObject, Vector3 clickPostion, Action<ShipAction> callback)
	{
		//open slect resource ui , with callback (resource) =>  OnResourceSelected(shipObject, interactableObject ,clickPostion,resource , Action<ShipAction> callback);

	}

	private void OnResourceSelected(ShipObject shipObject, InteractableObject interactableObject, Vector3 clickPostion, ResourceData resourceData, Action<ShipAction> callback)
	{
		IConsumer consumer = interactableObject.GetComponent<IConsumer>();
		DeliverAction deliverAction = new DeliverAction(this,shipObject, consumer, resourceData);
		callback?.Invoke(deliverAction);
	}
}
