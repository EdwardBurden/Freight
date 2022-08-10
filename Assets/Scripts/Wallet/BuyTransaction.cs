using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyTransaction : WalletTransaction
{
	private Building boughtFrom; //bought from
	private ResourceDefintion resourceBought;
	private Ship ship;
	public override string transactiontext { get { return $"{ship.GetObjectName()} spent {creditChange} Credits on a {resourceBought.resourceName} at {boughtFrom.GetObjectName()}"; } }

	public BuyTransaction(DateTime dateTime, int amount, Building boughtFrom, Ship ship, ResourceDefintion resourceBought) : base(dateTime, amount)
	{
		this.boughtFrom = boughtFrom;
		this.resourceBought = resourceBought;
		this.ship = ship;
	}

	public override int AmendCredits(int creditsTotal)
	{
		return creditsTotal - creditChange;
	}

	public override Transform GetTransactionLocation()
	{
		return boughtFrom.transform;
	}
}
