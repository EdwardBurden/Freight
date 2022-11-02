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

		private Vector3 startScale;
		private Vector3 startPos;

		private void Awake()
		{
			startScale = this.transform.localScale;
			startScale = this.transform.localScale;
		}

		public void OnBuildingHover()
		{
			this.transform.localScale *= 3;
		}

		public void OffBuildingHover()
		{
			this.transform.localScale = startScale;
		}


		internal void UpdateUI(string name, ResourceDefintion produce, int waterStored)
		{
			nameText.text = name;
			storageText.text = waterStored.ToString();
			resourceIconButtonWidget.ShowResource(produce);
		}
	}
}