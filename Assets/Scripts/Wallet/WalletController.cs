using System;
using UI;
using UnityEngine;

public class WalletController : Singleton<WalletController>
{
	public static event Action<WalletTransaction, int> TransactionAddedEvent;

	[SerializeField]
	public TransactionComplete_InCamera transactionIndicatorPrefab;
	private Wallet wallet;

	public void Awake()
	{
		_instance = this;
		wallet = new Wallet(); //temp
	}

	public int GetCreditsTotal()
	{
		return wallet.credits;
	}

	public void AddBuildingPlacementTransaction(int amount, string oldBuildingName, Building newBuilding)
	{
		BuildingPlacementTransaction buildingPlacement = new BuildingPlacementTransaction(DateTime.Now, amount, oldBuildingName, newBuilding);
		wallet.AddTransaction(buildingPlacement);
		TransactionAddedEvent?.Invoke(buildingPlacement, wallet.credits);
		CreateTransactionCompletePopup(buildingPlacement);
	}

	public void AddBuyTransaction(int amount, Building building, Ship ship, ResourceDefintion resource)
	{
		BuyTransaction buyTransaction = new BuyTransaction(DateTime.Now, amount, building, ship, resource);
		wallet.AddTransaction(buyTransaction);
		TransactionAddedEvent?.Invoke(buyTransaction, wallet.credits);
		CreateTransactionCompletePopup(buyTransaction);
	}

	public void AddSellTransaction(int amount, Building building, Ship ship, ResourceDefintion resource)
	{
		SellTransaction sellTransaction = new SellTransaction(DateTime.Now, amount, building, ship, resource);
		wallet.AddTransaction(sellTransaction);
		TransactionAddedEvent?.Invoke(sellTransaction, wallet.credits);
		CreateTransactionCompletePopup(sellTransaction);
	}

	private void CreateTransactionCompletePopup(WalletTransaction walletTransaction)
	{
		TransactionComplete_InCamera transactionCompleteInScene = GameUIController.instance.CreateInScene(transactionIndicatorPrefab, walletTransaction.GetTransactionLocation()) as TransactionComplete_InCamera;
		transactionCompleteInScene.ShowTransactionText(walletTransaction);
	}

}
