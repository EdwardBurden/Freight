using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/CollectActionDefintion")]

//TODO
public class CollectActionDefintion : ShipActionDefintion
{
	public override bool CanShowOption(ShipObject shipObject, InteractableObject interactableObject, Vector3 clickPostion)
	{
		IProducer producer = interactableObject.GetComponent<IProducer>();
		CarryObject carryBehaviour = shipObject.gameObject.GetComponent<CarryObject>();
		if (producer == null || carryBehaviour == null)
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
		IProducer producer = interactableObject.GetComponent<IProducer>();
		CollectAction collectAction = new CollectAction(this,shipObject, producer, resourceData);
		callback?.Invoke(collectAction);
	}
}
