using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TODO
[CreateAssetMenu(menuName = "ScriptableObject/MoveActionDefintion")]
public class MoveActionDefintion : ShipActionDefintion
{
	public override bool CanShowOption(ShipObject shipObject, InteractableObject interactableObject, Vector3 clickPostion)
	{
		return true;
	}

	public override void OnOptionSelected(ShipObject shipObject, InteractableObject interactableObject, Vector3 clickPostion, Action<ShipAction> callback)
	{
		callback.Invoke(new MoveAction(this, shipObject, clickPostion));
	}
}
