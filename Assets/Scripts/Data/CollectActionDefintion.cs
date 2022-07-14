using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/CollectActionDefintion")]

//TODO
public class CollectActionDefintion : ShipActionDefintion
{
	public override bool CanShowOption(ShipActionSelectionInfo shipActionSelectionInfo)
	{
		IProducer producer = shipActionSelectionInfo.interactable.GetComponent<IProducer>();
		CarryObject carryBehaviour = shipActionSelectionInfo.ship.gameObject.GetComponent<CarryObject>();
		if (producer == null || carryBehaviour == null)
		{
			return false;
		}
		return true;
	}



	public override void OnOptionSelected(ShipActionSelectionInfo shipActionSelectionInfo, Action<ShipAction> callback)
	{
		IProducer producer = shipActionSelectionInfo.interactable.GetComponent<IProducer>();
		//CollectResourceSelectScreen.Create(producer, OnResourceSelected);
		//open slect resource ui , with callback (resource) =>  OnResourceSelected(shipObject, interactableObject ,clickPostion,resource , Action<ShipAction> callback);

	}

	private void OnResourceSelected(ShipActionSelectionInfo shipActionSelectionInfo, ResourceData resourceData, Action<ShipAction> callback)
	{
		IProducer producer = shipActionSelectionInfo.interactable.GetComponent<IProducer>();
		CollectAction collectAction = new CollectAction(this, shipActionSelectionInfo.ship, producer, resourceData);
		callback?.Invoke(collectAction);
	}
}
