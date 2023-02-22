using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestCompleteTransaction : WalletTransaction
{
	private LevelBuilding building;

	public RequestCompleteTransaction(DateTime time, int creditChange, LevelBuilding building) : base(time, creditChange)
	{
		this.building = building;
	}

	public override string transactiontext { get { return $"Player completed {building.GetInteractableObjectName()}'s Request and was rewarded with {creditChange}"; } }

	public override int AmendCredits(int creditsTotal)
	{
		return creditsTotal + creditChange;
	}

	public override Transform GetTransactionLocation()
	{
		return building.transform;
	}
}
