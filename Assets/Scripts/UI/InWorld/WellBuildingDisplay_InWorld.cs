using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
namespace UI
{
	public class WellBuildingDisplay_InWorld : InWorld
	{
		[SerializeField] private TMP_Text nameText;
		[SerializeField] private TMP_Text storageText;
		[SerializeField] private ResourceIconButtonWidget resourceIconButtonWidget;

		internal void UpdateUI(string name, ResourceDefintion produce, int waterStored)
		{
			nameText.text = name;
			storageText.text = waterStored.ToString();
			resourceIconButtonWidget.ShowResource(produce);
		}
	}
}