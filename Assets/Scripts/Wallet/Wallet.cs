using System;
using UI;
using UnityEngine;

public class Wallet : MonoBehaviour
{
	public static event Action<WalletTransaction, int> TransactionAddedEvent;

	[SerializeField]
	public TransactionCompleteInScene transactionIndicatorPrefab;
	private PlayerTransactions wallet;

	public void Awake()
	{
		wallet = new PlayerTransactions(); //temp
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
		CreateTransactionCompletePopup(buyTransaction);
	}

	public void CreateAndAddSellTransaction(int amount, Building building, Ship ship, ResourceDefintion resource)
	{
		SellTransaction sellTransaction = new SellTransaction(DateTime.Now, amount, building, ship, resource);
		wallet.AddTransaction(sellTransaction);
		TransactionAddedEvent?.Invoke(sellTransaction, wallet.credits);
		CreateTransactionCompletePopup(sellTransaction);
	}

	private void CreateTransactionCompletePopup(WalletTransaction walletTransaction)
	{
		TransactionCompleteInScene transactionCompleteInScene = UIManager.instance.CreateInScene(transactionIndicatorPrefab, walletTransaction.GetTransactionLocation()) as TransactionCompleteInScene;
		transactionCompleteInScene.SetText(walletTransaction);
	}

}
