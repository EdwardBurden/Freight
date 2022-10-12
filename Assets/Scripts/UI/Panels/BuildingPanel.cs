using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;

public class BuildingPanel : Panel
{
	[SerializeField]
	private TMP_Text nameField;

	private Building building;

	protected void Start()
	{
		BuildingController.BuildingSelectedEvent += BuildingController_BuildingSelectedEvent; 
		BuildingController.BuildingDeSelectedEvent += BuildingController_BuildingDeSelectedEvent; 
	}

	private void BuildingController_BuildingDeSelectedEvent(Building obj)
	{
		Hide();
	}

	private void BuildingController_BuildingSelectedEvent(Building obj)
	{
		Hide();
		building = obj;
		nameField.text = building.GetObjectName();
		Show();
	}

	private void OnDestroy()
	{
		BuildingController.BuildingSelectedEvent -= BuildingController_BuildingSelectedEvent;
		BuildingController.BuildingDeSelectedEvent -= BuildingController_BuildingDeSelectedEvent;
	}
}
