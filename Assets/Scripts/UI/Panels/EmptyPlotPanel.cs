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
		EmptyPlot empty = building as EmptyPlot;
		empty.SwitchBuilding(selectedBuilding);
		Hide();
	}

	protected override void OnBuildingSelected(Building building)
	{
		if (building is EmptyPlot empty)
		{
			base.OnBuildingSelected(building);
			ShowBuildingOptions(empty);
		}
	}

	private void ShowBuildingOptions(EmptyPlot empty)
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
