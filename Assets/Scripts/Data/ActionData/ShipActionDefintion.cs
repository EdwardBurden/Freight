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

	public abstract bool CanShowOption(RightClickContextActionInfo shipActionSelectionInfo);

	public abstract ActionPreview CreateActionPreview(ShipObject ship, Action<ShipAction> callback);//user has selected this action on action list panel, what to do next?

	public virtual void OnDefaultRightClickMenuOption(RightClickContextActionInfo shipActionSelectionInfo, Action<ShipAction> callback)//user has selected this action on right click menu where its the only option, what to do next?
	{
		OnOptionSelectedFromRightClickMenu(shipActionSelectionInfo, callback);
	} 

	public abstract void OnOptionSelectedFromRightClickMenu(RightClickContextActionInfo shipActionSelectionInfo, Action<ShipAction> callback); //user has selected this action on right click menu, what to do next?
}
