using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
	[SerializeField]
	private BuildingMenu buildingMenu;
	private Building buildingSelected;

	private void Awake()
	{
		MouseInteractionTracker.mouseLeftClickedInteractable += OnLeftClickInteractable;
		MouseInteractionTracker.mouseLeftClickedNothing += OnLeftClickNotInteractable;
	}

	private void OnLeftClickNotInteractable()
	{
		CloseBuildingMenu();
		//throw new NotImplementedException();
	}

	private void OnLeftClickInteractable(InteractableObject obj)
	{
		if (obj is Building building)
		{
			OpenBuildingMenu(building);
			//	SelectControllableObject(controllableObject);
		}
		//throw new NotImplementedException();
	}

	public void OpenBuildingMenu(Building building)
	{
		buildingMenu.Display(building);
	}

	public void CloseBuildingMenu()
	{
		buildingMenu.Close();

	}
}
