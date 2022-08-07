using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalletManager : Singleton<WalletManager>
{
	public event Action<WalletTransaction, int> TransactionAddedEvent;

	private Wallet wallet;

	public void Awake()
	{
		_instance = this;
		wallet = new Wallet();
	}

	public int GetCreditsTotal()
	{
		return wallet.credits;
	}

	public void CreateAndAddBuyTransaction(int amount, Building building, Ship ship, ResourceDefintion resource)
	{
		BuyTransaction buyTransaction = new BuyTransaction(DateTime.Now, amount, building, ship, resource);
		wallet.AddTransaction(buyTransaction);
		TransactionAddedEvent?.Invoke(buyTransaction, wallet.credits);
	}

	public void CreateAndAddSellTransaction(int amount, Building building, Ship ship, ResourceDefintion resource)
	{
		SellTransaction sellTransaction = new SellTransaction(DateTime.Now, amount, building, ship, resource);
		wallet.AddTransaction(sellTransaction);
		TransactionAddedEvent?.Invoke(sellTransaction, wallet.credits);
	}

}
