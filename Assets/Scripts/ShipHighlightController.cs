using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ShipHighlightController : Singleton<ShipHighlightController>
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

	private void OnEnable()
	{
		MouseInput.mouseHoveredOnInteractable += MouseInput_mouseHoveredOnInteractable;
		MouseInput.mouseHoveredOffInteractable += MouseInput_mouseHoveredOffInteractable;


		ShipSelectionController.ShipSelectedEvent += ShipSelectionController_ShipSelectedEvent;
		ShipSelectionController.ShipDeSelectedEvent += ShipSelectionController_ShipDeSelectedEvent;
	}

	private void MouseInput_mouseHoveredOffInteractable(InteractableObject obj)
	{
		if (hoverOutline == null)
		{
			return;
		}
		if (obj.GetComponentInChildren<Outline>() != selectOutline)
		{
			hoverOutline.OutlineColor = defaultColour;
			hoverOutline = null;
		}
	}

	private void MouseInput_mouseHoveredOnInteractable(InteractableObject obj)
	{
		if (obj is Ship && obj.GetComponentInChildren<Outline>() != selectOutline)
		{
			hoverOutline = obj.gameObject.GetComponentInChildren<Outline>();
			hoverOutline.OutlineColor = hoverColour;
		}
	}

	private void OnDisable()
	{
		MouseInput.mouseHoveredOnInteractable -= MouseInput_mouseHoveredOnInteractable;
		MouseInput.mouseHoveredOffInteractable -= MouseInput_mouseHoveredOffInteractable;

		ShipSelectionController.ShipSelectedEvent -= ShipSelectionController_ShipSelectedEvent;
		ShipSelectionController.ShipDeSelectedEvent -= ShipSelectionController_ShipDeSelectedEvent;
	}

	private void ShipSelectionController_ShipDeSelectedEvent(Ship obj)
	{
		if (selectOutline == null)
		{
			return;
		}
		selectOutline.OutlineColor = defaultColour;
		selectOutline = null;
	}

	private void ShipSelectionController_ShipSelectedEvent(Ship obj)
	{
		selectOutline = obj.gameObject.GetComponentInChildren<Outline>();
		selectOutline.OutlineColor = selctColour;
		hoverOutline = null;
	}

}
