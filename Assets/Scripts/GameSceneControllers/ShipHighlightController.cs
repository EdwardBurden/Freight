using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ShipHighlightController : Singleton<ShipHighlightController>, IGameController
{
	public Color defaultColour;

	private Outline selectOutline;
	private Outline hoverOutline;

	public Color selctColour;
	public Color hoverColour;

	private void Awake()
	{
		_instance = this;
	}

	public void Init()
	{
		ShipDeselected(null);
		OnMouseOff(null);
	}

	private void OnEnable()
	{
		MouseInput.mouseHoveredOnInteractable += OnMouseOver;
		MouseInput.mouseHoveredOffInteractable += OnMouseOff;

		ShipSelectionController.ShipSelectedEvent += ShipSelected;
		ShipSelectionController.ShipDeSelectedEvent += ShipDeselected;
	}

	private void OnMouseOff(InteractableObject interactableObject)
	{
		if (hoverOutline == null)
		{
			return;
		}
		if (interactableObject != null && interactableObject is Ship ship && ship.outline != selectOutline)
		{
			hoverOutline.OutlineColor = defaultColour;
			hoverOutline.enabled = false;
			hoverOutline = null;
		}
	}

	private void OnMouseOver(InteractableObject interactableObject)
	{
		if (interactableObject is Ship ship && ship.outline != selectOutline)
		{
			hoverOutline = ship.outline;
			hoverOutline.OutlineColor = hoverColour;
			hoverOutline.enabled = true;
		}
	}

	private void OnDisable()
	{
		MouseInput.mouseHoveredOnInteractable -= OnMouseOver;
		MouseInput.mouseHoveredOffInteractable -= OnMouseOff;

		ShipSelectionController.ShipSelectedEvent -= ShipSelected;
		ShipSelectionController.ShipDeSelectedEvent -= ShipDeselected;
	}

	private void ShipDeselected(Ship ship)
	{
		if (selectOutline == null)
		{
			return;
		}
		selectOutline.OutlineColor = defaultColour;
		selectOutline.enabled = false;
		selectOutline = null;
	}

	private void ShipSelected(Ship ship)
	{
		if (hoverOutline != null)
		{
			hoverOutline.enabled = false;
			hoverOutline = null;
		}

		selectOutline = ship.outline;
		selectOutline.OutlineColor = selctColour;
		selectOutline.enabled = true;
	}
}
