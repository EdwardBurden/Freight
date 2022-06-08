using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
	public Transform canvas;

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
		else {
			CloseBuildingMenu();
		}
		//throw new NotImplementedException();
	}

	public void OpenBuildingMenu(Building building)
	{
		CloseBuildingMenu();

		buildingMenu = building.CreateBuildingMenu(canvas);
		buildingMenu.Display(building);
	}

	public void CloseBuildingMenu()
	{
		if (buildingMenu != null)
		{
			buildingMenu.Close();
			Destroy(buildingMenu.gameObject);
		}

	}
}
