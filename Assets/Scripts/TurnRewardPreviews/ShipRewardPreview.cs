using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRewardPreview : MonoBehaviour, IGamePreview
{
	public event Action FinishedPreviewEvent;

	private Action<bool> callback;
	private ShipDefintion shipRewardDefintion;

	public void OnLeftClickInteractable(InteractableObject interactableObject, Vector3 worldMousePosition)
	{
		//throw new NotImplementedException();
	}

	public void OnRightClickInteractable(InteractableObject interactableObject, Vector3 worldMousePosition)
	{
		//throw new NotImplementedException();
	}

	public void Init(ShipDefintion shipRewardDefintion, Action<bool> callback)
	{
		this.shipRewardDefintion = shipRewardDefintion;
		this.callback = callback;
	}

	public void UpdatePreview()
	{
		shipRewardDefintion.CreateShip();
		FinishedPreviewEvent?.Invoke();
		callback.Invoke(true);
	}

	public void DestroyPreview()
	{
		Destroy(gameObject);
	}
}
