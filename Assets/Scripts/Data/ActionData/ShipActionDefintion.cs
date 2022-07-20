using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
//TODO
public abstract class ShipActionDefintion : ScriptableObject
{
	public string actionName;
	public Sprite actionIcon;
	public Color actionColor;
	public int uiOrder;
	public ShipActionIconButtonWidget iconPrefab;

	public abstract bool CanShowOption(ShipActionSelectionInfo shipActionSelectionInfo);

	public abstract void OnOptionSelected(ShipActionSelectionInfo shipActionSelectionInfo, Action<ShipAction> callback);
}
