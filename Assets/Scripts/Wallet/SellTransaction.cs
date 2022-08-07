using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellTransaction : WalletTransaction
{
	private Building soldTo; //bought from
	private ResourceDefintion resourceSold;
	private Ship ship;
	public override string transactiontext { get { return $"{ship.GetObjectName()} received {creditChange} Credits for a {resourceSold.resourceName} at {soldTo.GetObjectName()}"; } }

	public SellTransaction(DateTime dateTime, int amount, Building soldTo, Ship ship, ResourceDefintion resourceSold) : base(dateTime, amount)
	{
		this.soldTo = soldTo;
		this.resourceSold = resourceSold;
		this.ship = ship;
	}

	public override int AmendCredits(int creditsTotal)
	{
		return creditsTotal + creditChange;
	}
}
