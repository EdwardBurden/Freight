using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/MoveActionDefintion")]
public class MoveActionDefintion : ShipActionDefintion
{
	public override bool CanShowOption(ShipObject shipObject, InteractableObject interactableObject, Vector3 clickPostion)
	{
		return true;
	}



	public override void OnOptionSelected(ShipObject shipObject, InteractableObject interactableObject, Vector3 clickPostion, Action<ShipAction> callback)
	{
		MoveAction moveAction = new MoveAction(shipObject, clickPostion);
		callback.Invoke(moveAction);
		//open slect resource ui , with callback (resource) =>  OnResourceSelected(shipObject, interactableObject ,clickPostion,resource , Action<ShipAction> callback);
	}
}
