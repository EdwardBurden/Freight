using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TODO
public abstract class ShipActionDefintion : ScriptableObject
{
	public string actionName;
	public GameObject iconPrefab;

	public abstract bool CanShowOption(ShipActionSelectionInfo shipActionSelectionInfo);

	public abstract void OnOptionSelected(ShipActionSelectionInfo shipActionSelectionInfo, Action<ShipAction> callback);


	public virtual GameObject CreateActionSelectionButton(Transform options)
	{
		return Instantiate(iconPrefab, options);
	}
}
