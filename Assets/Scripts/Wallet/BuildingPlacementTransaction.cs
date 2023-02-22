using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacementTransaction : WalletTransaction
{
	private string oldBuildingName;
	private LevelBuilding newBuilding;

	public override string transactiontext { get { return $"Player spent {creditChange} Credits on to replace {oldBuildingName} with {newBuilding.GetInteractableObjectName()}"; } }

	public BuildingPlacementTransaction(DateTime time, int creditChange, string oldBuilding, LevelBuilding newBuilding) : base(time, creditChange)
	{
		this.oldBuildingName = oldBuilding;
		this.newBuilding = newBuilding;
	}

	public override int AmendCredits(int creditsTotal)
	{
		return creditsTotal - creditChange;
	}

	public override Transform GetTransactionLocation()
	{
		return newBuilding.transform;
	}
}
