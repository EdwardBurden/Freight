using System;
using UI;
using UnityEngine;

public class WalletController : Singleton<WalletController>, IGameController
{
	public static event Action<WalletTransaction, int> TransactionAddedEvent;

	[SerializeField] public DisplayMessage_InCamera transactionIndicatorPrefab;
	private Wallet wallet;

	public void Awake()
	{
		_instance = this;
	}

	public void Init()
	{
		wallet = new Wallet();
	}

	public int GetCreditsTotal()
	{
		return wallet.credits;
	}

	public void AddBuildingPlacementTransaction(int amount, string oldBuildingName, Building newBuilding)
	{
		BuildingPlacementTransaction buildingPlacement = new BuildingPlacementTransaction(DateTime.Now, amount, oldBuildingName, newBuilding);
		AddTransaction(buildingPlacement);
	}

	public void AddSettlementRequestFulfilledTransaction(int amount, Building building)
	{
		//TODO
		RequestCompleteTransaction requestComplete = new RequestCompleteTransaction(DateTime.Now, amount, building);
		AddTransaction(requestComplete);
	}

	public void AddBuyTransaction(int amount, Building building, Ship ship, ResourceDefintion resource)
	{
		BuyTransaction buyTransaction = new BuyTransaction(DateTime.Now, amount, building, ship, resource);
		AddTransaction(buyTransaction);
	}

	public void AddSellTransaction(int amount, Building building, Ship ship, ResourceDefintion resource)
	{
		SellTransaction sellTransaction = new SellTransaction(DateTime.Now, amount, building, ship, resource);
		AddTransaction(sellTransaction);
	}

	private void AddTransaction(WalletTransaction walletTransaction)
	{
		wallet.AddTransaction(walletTransaction);
		TransactionAddedEvent?.Invoke(walletTransaction, wallet.credits);
		DisplayMessage_InCamera transactionCompleteInScene = GameUIController.instance.CreateInScene(transactionIndicatorPrefab, walletTransaction.GetTransactionLocation()) as DisplayMessage_InCamera;
		transactionCompleteInScene.ShowTransactionText(walletTransaction.transactiontext);
	}
}
