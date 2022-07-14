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
	public ShipActionIconButtonWidget iconPrefab;

	public abstract bool CanShowOption(ShipActionSelectionInfo shipActionSelectionInfo);

	public abstract void OnOptionSelected(ShipActionSelectionInfo shipActionSelectionInfo, Action<ShipAction> callback);


	public virtual ShipActionIconButtonWidget CreateActionSelectionButton(Transform options , Action callback)
	{
		ShipActionIconButtonWidget shipActionIconWidget = Instantiate(iconPrefab, options);
		shipActionIconWidget.ShowAction(this, callback);
		return shipActionIconWidget;
	}
}
