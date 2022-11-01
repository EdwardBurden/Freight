using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace UI
{
	public class TransactionComplete_InCamera : InCamera
	{
		[SerializeField]
		private TMP_Text transactionText;

		private int time = 3;

		private void Start()
		{
			StartCoroutine(Die());
		}

		private IEnumerator Die()
		{
			yield return new WaitForSeconds(time);
			Destroy(this.gameObject);
		}

		public void ShowTransactionText(WalletTransaction walletTransaction)
		{
			transactionText.text = walletTransaction.transactiontext;
		}
	}
}