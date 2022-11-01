using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace UI
{
	public class Wallet_HUD : HUD
	{
		[SerializeField] private TMP_Text priceText;

		private void OnEnable()
		{
			WalletController.TransactionAddedEvent += OnTransactionAdded;
		}

		private void OnDisable()
		{
			WalletController.TransactionAddedEvent -= OnTransactionAdded;
		}

		private void OnTransactionAdded(WalletTransaction transaction, int totalCredits)
		{
			priceText.text = totalCredits.ToString();
		}
	}
}
