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
			WalletManager.instance.TransactionAddedEvent += Instance_TransactionAddedEvent;
			priceField.text = WalletManager.instance.GetCreditsTotal().ToString();
		}

		private void Instance_TransactionAddedEvent(WalletTransaction transaction, int credits)
		{
			priceField.text = credits.ToString();
		}

		private void OnDestroy()
		{
			WalletManager.instance.TransactionAddedEvent -= Instance_TransactionAddedEvent;
		}
	}
}
