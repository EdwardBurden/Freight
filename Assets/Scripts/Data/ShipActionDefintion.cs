using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TODO
public abstract class ShipActionDefintion : ScriptableObject
{
	public string actionName;
	public GameObject iconPrefab;

	public abstract bool CanShowOption(ShipObject shipObject, InteractableObject interactableObject, Vector3 clickPostion);

	public abstract void OnOptionSelected(ShipObject shipObject, InteractableObject interactableObject, Vector3 clickPostion, Action<ShipAction> callback);


	public virtual GameObject CreateActionSelectionButton(Transform options)
	{
		return Instantiate(iconPrefab, options);
	}
}
