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

		private void Start()
		{
			Wallet.TransactionAddedEvent += Instance_TransactionAddedEvent;
			priceField.text = SystemsManager.instance.wallet.GetCreditsTotal().ToString();
		}

		private void Instance_TransactionAddedEvent(WalletTransaction transaction, int credits)
		{
			priceField.text = credits.ToString();
		}

		private void OnDestroy()
		{
			Wallet.TransactionAddedEvent -= Instance_TransactionAddedEvent;
		}
	}
}
