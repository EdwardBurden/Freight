using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingIconButtonWidget : MonoBehaviour
{
	[SerializeField]
	private TMP_Text text;

	[SerializeField]
	private Image icon;

	[SerializeField]
	private Image backgroundIcon;

	[SerializeField]
	private Button button;

	public void ShowBuilding(BuildingLevelDefintion building, Action callback = null)
	{
		text.text = building.buildingName;
		icon.sprite = building.buildingIcon;

		if (callback != null)
		{
			button.onClick.AddListener(() => callback());
		}
		button.interactable = callback != null;
	}
}
