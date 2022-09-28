using System.Collections.Generic;

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
