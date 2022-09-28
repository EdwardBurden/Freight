using System;
using UnityEngine;

public abstract class ShipActionDefintion : ScriptableObject
{
	public string actionName;
	public Sprite actionIcon;
	public Color actionColor;
	public int uiOrder;

	public abstract bool CanBeCreated(Ship shipObject, InteractableObject interactableObject, Vector3 postion);

	public abstract void CreateActionPreview(Ship ship, Action<ShipOrder> callback);//user has selected this action on action list panel, what to do next?

	public abstract void ActionSelected(Ship shipObject, InteractableObject interactableObject, Vector3 postion, Action<ShipOrder> callback);//user has selected this action on right click menu where its the only option, what to do next?
}
