using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;
using System;

public class EmptyPlotPanel : BuildingPanel<EmptyPlot>
{
	[SerializeField] private BuildingIconButtonWidget buildingIconButtonWidget;
	[SerializeField] private Transform buildingOptions;

	public override void Show()
	{
		base.Show();
		GameObjectUtils.Clear(buildingOptions);
		foreach (BuildingDefintion building in building.GetOptions())
		{
			ShowBuildingoption(building);
		}
	}

	public void ChangeBuilding(BuildingDefintion selectedBuilding)
	{
		EmptyPlot empty = building as EmptyPlot;
		empty.SwitchBuilding(selectedBuilding);
		Hide();
	}

	private void ShowBuildingoption(BuildingDefintion building)
	{
		BuildingIconButtonWidget widget = Instantiate(buildingIconButtonWidget, buildingOptions);
		widget.ShowBuilding(building, () => ChangeBuilding(building));
	}
}
