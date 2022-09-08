using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransactions //to be saved
{
	public int credits { get; private set; }
	public List<WalletTransaction> walletTransactions = new List<WalletTransaction>();

	public void AddTransaction(WalletTransaction transaction)
	{
		walletTransactions.Add(transaction);
		credits = transaction.AmendCredits(credits);
	}
}
