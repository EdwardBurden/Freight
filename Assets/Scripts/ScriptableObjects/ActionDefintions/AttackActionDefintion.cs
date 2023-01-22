using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/AttackActionDefintion")]
public class AttackActionDefintion : ShipActionDefintion
{
	public override void ActionSelected(Ship shipObject, InteractableObject interactableObject, Vector3 postion, Action<ShipOrder> callback)
	{
		throw new NotImplementedException();
	}

	public override bool CanBeCreated(Ship shipObject, InteractableObject interactableObject, Vector3 postion)
	{
		throw new NotImplementedException();
	}

	public override void CreateActionPreview(Ship ship, Action<ShipOrder> callback)
	{
		throw new NotImplementedException();
	}
}
