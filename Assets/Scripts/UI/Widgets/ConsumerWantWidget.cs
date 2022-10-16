using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
namespace UI
{
	public class ConsumerWantWidget : MonoBehaviour
	{
		[SerializeField]
		private Image background;

		[SerializeField]
		private Image icon;

		[SerializeField]
		private TMP_Text number;

		public void Init(ConsumerWant consumerWant)
		{
			this.icon.sprite = consumerWant.resource.resourceIcon;
			background.color = consumerWant.resource.resourceColor;
			number.text = $"{consumerWant.delivered}/{consumerWant.amount}";
		}
	}


}
