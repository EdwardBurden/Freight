using System;
using UnityEngine;

public class EmptyPlot : Building
{
	[SerializeField]
	public Building[] options;

	public void SwitchBuilding(Building selectedBuilding)
	{
		//TODO rename to try, check player wallet to see if they can switch 
		Building newBuilding = BuildingController.instance.ReplaceBuilding(this, selectedBuilding);
		WalletController.instance.AddBuildingPlacementTransaction(selectedBuilding.buildCost, this.GetObjectName(), newBuilding);
	}
}
