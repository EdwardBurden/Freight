using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{//TODO
	public class ShipActionIconButtonWidget : MonoBehaviour //can override for collect/deliver to show resource
	{
		[SerializeField]
		private TMP_Text text;

		[SerializeField]
		private Image icon;

		[SerializeField]
		private Image backgroundIcon;

		[SerializeField]
		private Button button;

		public void ShowAction(ShipActionDefintion actionDefintion, Action callback)
		{
			text.text = actionDefintion.actionName;
			icon.sprite = actionDefintion.actionIcon;
			backgroundIcon.color = actionDefintion.actionColor;
			button.onClick.AddListener(() => callback());
		}
	}
}