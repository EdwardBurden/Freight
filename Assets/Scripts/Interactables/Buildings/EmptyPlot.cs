using System;
using System.Collections.Generic;
using UnityEngine;

public class EmptyPlot : Building
{
	private EmptyDefintion emptyDefintion => buildingDefintion as EmptyDefintion;

	public override void Init(BuildingDefintion buildingDefintion)
	{
		base.Init(buildingDefintion);
	}

	public void SwitchBuilding(BuildingDefintion selectedBuilding)
	{
		//TODO rename to try, check player wallet to see if they can switch 
		Building newBuilding = BuildingController.instance.ReplaceBuilding(this, selectedBuilding);
		WalletController.instance.AddBuildingPlacementTransaction(selectedBuilding.buildCost, this.GetObjectName(), newBuilding);
	}

	internal IEnumerable<BuildingDefintion> GetOptions()
	{
		return emptyDefintion.options;
	}
}
