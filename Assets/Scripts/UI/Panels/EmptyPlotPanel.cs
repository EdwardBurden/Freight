using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;
using System;

public class EmptyPlotPanel : BuildingPanel
{
	[SerializeField]
	private BuildingIconButtonWidget buildingIconButtonWidget;

	[SerializeField]
	private Transform buildingOptions;

	public void ChangeBuilding(Building selectedBuilding)
	{
		//TODO go to building, pay cost to build
		//TODO go to building controller to replace
	}

	protected override void OnBuildingSelected(Building building)
	{
		if (building is Empty empty)
		{
			base.OnBuildingSelected(building);
			ShowBuildingOptions(empty);
		}
	}

	private void ShowBuildingOptions(Empty empty)
	{
		GameObjectUtils.Clear(buildingOptions);
		foreach (Building building in empty.options)
		{
			ShowBuildingoption(building);
		}
	}

	private void ShowBuildingoption(Building building)
	{
		BuildingIconButtonWidget widget = Instantiate(buildingIconButtonWidget, buildingOptions);
		widget.ShowBuilding(building, () => ChangeBuilding(building));
	}
}
