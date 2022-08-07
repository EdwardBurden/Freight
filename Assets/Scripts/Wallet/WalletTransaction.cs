using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WalletTransaction
{
	public DateTime time; //Can be changed to in game time later
	public int creditChange;

	public abstract string transactiontext { get;  }//Bought a iron , sold a a silicon

	public WalletTransaction(DateTime time, int creditChange)
	{
		this.time = time;
		//this.transactiontext = transactiontext;
		//this.building = building;
		//this.ship = ship;
		this.creditChange = creditChange;
	}

	public abstract int AmendCredits(int creditsTotal);

}
