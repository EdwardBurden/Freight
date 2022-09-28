using System;
using UnityEngine;

public abstract class WalletTransaction
{
	public DateTime time;
	public int creditChange;

	public abstract string transactiontext { get;  }

	public WalletTransaction(DateTime time, int creditChange)
	{
		this.time = time;
		this.creditChange = creditChange;
	}

	public abstract int AmendCredits(int creditsTotal);

	public abstract Transform GetTransactionLocation();

}
