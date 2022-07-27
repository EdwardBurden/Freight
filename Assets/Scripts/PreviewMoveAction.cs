using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class PreviewMoveAction : Singleton<PreviewMoveAction> //or created in the scen froma prefab and killed when doen, can decide later
{
	private ShipObject shipObject;
	private Action<Vector3> callback;

	private GameObject cube;

	private bool open;

	private void Awake()
	{
		_instance = this;
	}

	public void RegisterEvents()
	{
		MouseInteractionTracker.instance.mousePostion += Instance_mousePostion;
		MouseInteractionTracker.instance.LeftClickInteractableEvent += OnLeftClickInteractable;
		MouseInteractionTracker.instance.RightClickInteractableEvent += OnRightClickInteractable;
	}

	private void Instance_mousePostion(Vector3 postion)
	{
		cube.transform.position = postion;
	}

	public void UnregisterEvents()
	{
		MouseInteractionTracker.instance.mousePostion -= Instance_mousePostion;
		MouseInteractionTracker.instance.LeftClickInteractableEvent -= OnLeftClickInteractable;
		MouseInteractionTracker.instance.RightClickInteractableEvent -= OnRightClickInteractable;
	}

	private void OnRightClickInteractable(InteractableObject arg1, Vector3 arg2)
	{
		//throw new NotImplementedException();
	}

	private void OnLeftClickInteractable(InteractableObject arg1, Vector3 arg2)
	{
		callback.Invoke(arg2);
		Close();
		//throw new NotImplementedException();
	}

	public void Open(ShipObject shipObject, Action<Vector3> callback)
	{
		if (open)
		{
			Close();
		}
		this.callback = callback;
		this.shipObject = shipObject;
		open = true;

		ShipSelectionManager.instance.UnregisterEvents();
		UIManager.instance.CloseAllScreens();
		RegisterEvents();
		cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube.GetComponent<Collider>().enabled = false;
	}

	private void Close()
	{
		open = false;
		ShipSelectionManager.instance.RegisterEvents();
		UnregisterEvents();
		Destroy(cube.gameObject);
	}
}
