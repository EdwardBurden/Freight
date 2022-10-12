using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace UI
{
	public class WalletHUD : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text priceField;

		private void OnEnable()
		{
			WalletController.TransactionAddedEvent += Instance_TransactionAddedEvent;
			//Wallet wallet = GameObject.FindObjectOfType<Wallet>();
			//priceField.text = wallet.GetCreditsTotal().ToString();
		}

		private void Instance_TransactionAddedEvent(WalletTransaction transaction, int credits)
		{
			priceField.text = credits.ToString();
		}

		private void OnDisable()
		{
			WalletController.TransactionAddedEvent -= Instance_TransactionAddedEvent;
		}
	}
}
