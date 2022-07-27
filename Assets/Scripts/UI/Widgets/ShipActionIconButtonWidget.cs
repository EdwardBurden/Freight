using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	//Used for when a user slects an action in the ShipActionOptionsScreen
	public class ShipActionIconButtonWidget : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text text;

		[SerializeField]
		private Image icon;

		[SerializeField]
		private Image backgroundIcon;

		[SerializeField]
		private Button button;

		public void ShowAction(ShipActionDefintion actionDefintion, Action callback, bool interactive = true)
		{
			text.text = actionDefintion.actionName;
			icon.sprite = actionDefintion.actionIcon;
			backgroundIcon.color = actionDefintion.actionColor;
			button.onClick.AddListener(() => callback());
			button.interactable = interactive;
		}
	}
}