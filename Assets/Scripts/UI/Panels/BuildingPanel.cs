using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;

public class BuildingPanel : Panel
{
	[SerializeField]
	private TMP_Text nameField;

	Building building;

	protected void OnEnable()
	{
		BuildingController.BuildingSelectedEvent += OnBuildingSelected;
		BuildingController.BuildingDeSelectedEvent += OnBuildingDeSelected;
	}

	private void OnDisable()
	{
		BuildingController.BuildingSelectedEvent -= OnBuildingSelected;
		BuildingController.BuildingDeSelectedEvent -= OnBuildingDeSelected;
	}

	protected virtual void OnBuildingDeSelected(Building building)
	{
		Hide();
	}

	protected virtual void OnBuildingSelected(Building building)
	{
		this.building = building;
		Show();
	}

	public override void Show()
	{
		nameField.text = building.GetObjectName();
		base.Show();
	}
}
