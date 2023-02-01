using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipUpgradePreview : MonoBehaviour, IGamePreview
{
	public event Action FinishedPreviewEvent;

	public GameObject selectionCircle;
	private Action<Ship> callback;

	public void Start()
	{
		MouseInput.mouseHoveredOnInteractable += OnMouseHoveredOnInteractable;
		MouseInput.mouseHoveredOffInteractable += OnMouseHoveredOffInteractable;
	}

	public void OnDestroy()
	{
		MouseInput.mouseHoveredOnInteractable -= OnMouseHoveredOnInteractable;
		MouseInput.mouseHoveredOffInteractable -= OnMouseHoveredOffInteractable;
	}

	public void Init(int speedIncrease, Action<Ship> value)
	{
		callback = value;
		selectionCircle.GetComponent<MeshRenderer>().enabled = false;
	}

	private void OnMouseHoveredOffInteractable(InteractableObject interactableObject)
	{
		selectionCircle.GetComponent<MeshRenderer>().enabled = false;
	}

	private void OnMouseHoveredOnInteractable(InteractableObject interactableObject)
	{
		selectionCircle.GetComponent<MeshRenderer>().enabled = (interactableObject is Ship);
		selectionCircle.transform.position = interactableObject.transform.position;
	}

	public void OnLeftClickInteractable(InteractableObject interactableObject, Vector3 worldMousePosition)
	{
		if (interactableObject is Ship ship)
		{
			FinishedPreviewEvent?.Invoke();
			callback?.Invoke(ship);
		}
	}

	public void OnRightClickInteractable(InteractableObject interactableObject, Vector3 worldMousePosition)
	{
		//throw new NotImplementedException();
	}

	public void UpdatePreview()
	{
		//throw new NotImplementedException();
	}

	public void DestroyPreview()
	{
		Destroy(gameObject);
	}
}
