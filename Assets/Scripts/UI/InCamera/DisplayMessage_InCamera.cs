using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace UI
{
	public class DisplayMessage_InCamera : MonoBehaviour
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

		public void ShowTransactionText(string message, int length = 3)
		{
			transactionText.text = message;
			time = length;
		}
	}
}