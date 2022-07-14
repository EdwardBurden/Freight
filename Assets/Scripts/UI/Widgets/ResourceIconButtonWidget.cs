using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{//TODO
	public class ResourceIconButtonWidget : MonoBehaviour //can override for collect/deliver to show resource
	{
		[SerializeField]
		private TMP_Text text;

		[SerializeField]
		private Image icon;

		[SerializeField]
		private Image backgroundIcon;

		[SerializeField]
		private Button button;

		public void ShowResource(ResourceData resourceData, Action callback)
		{
			text.text = resourceData.resourceName;
			icon.sprite = resourceData.resourceIcon;
			backgroundIcon.color = resourceData.resourceColor;
			button.onClick.AddListener(() => callback());
		}
	}
}