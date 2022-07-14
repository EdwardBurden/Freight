using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TODO
[CreateAssetMenu(menuName = "ScriptableObject/DeliverActionDefintion")]
public class DeliverActionDefintion : ShipActionDefintion
{
	public override bool CanShowOption(ShipActionSelectionInfo shipActionSelectionInfo)
	{
		IConsumer consumer = shipActionSelectionInfo.interactable.GetComponent<IConsumer>();
		CarryObject carryBehaviour = shipActionSelectionInfo.ship.gameObject.GetComponent<CarryObject>();
		if (consumer == null || carryBehaviour == null)
		{
			return false;
		}
		return true;
	}



	public override void OnOptionSelected(ShipActionSelectionInfo shipActionSelectionInfo, Action<ShipAction> callback)
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
