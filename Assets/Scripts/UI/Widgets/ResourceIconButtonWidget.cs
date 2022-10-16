using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{//Used for when a user slects a resource in the ResourceSelectScreen
	public class ResourceIconButtonWidget : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text text;

		[SerializeField]
		private Image icon;

		[SerializeField]
		private Image backgroundIcon;

		[SerializeField]
		private Button button;

		public void ShowResource(ResourceDefintion resourceData, Action callback = null)
		{
			text.text = resourceData.resourceName;
			icon.sprite = resourceData.resourceIcon;
			backgroundIcon.color = resourceData.resourceColor;

			if (callback != null)
			{
				button.onClick.AddListener(() => callback());
			}
			button.interactable = callback != null;
		}
	}
}