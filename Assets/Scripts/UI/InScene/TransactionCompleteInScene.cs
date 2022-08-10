using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace UI
{
	public class TransactionCompleteInScene : InScene
	{
		[SerializeField]
		private TMP_Text text;

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

		internal void SetText(WalletTransaction walletTransaction)
		{
			text.text = walletTransaction.transactiontext;
		}
	}
}