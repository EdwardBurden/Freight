using System;
using UnityEngine;

public class SellTransaction : WalletTransaction
{
	private LevelBuilding soldTo; //bought from
	private ResourceDefintion resourceSold;
	private Ship ship;
	public override string transactiontext { get { return $"{ship.GetInteractableObjectName()} received {creditChange} Credits for a {resourceSold.resourceName} at {soldTo.GetInteractableObjectName()}"; } }

	public SellTransaction(DateTime dateTime, int amount, LevelBuilding soldTo, Ship ship, ResourceDefintion resourceSold) : base(dateTime, amount)
	{
		this.soldTo = soldTo;
		this.resourceSold = resourceSold;
		this.ship = ship;
	}

	public override int AmendCredits(int creditsTotal)
	{
		return creditsTotal + creditChange;
	}

	public override Transform GetTransactionLocation()
	{
		return soldTo.transform;
	}
}
