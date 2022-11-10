using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
namespace UI
{
	public  class SummaryBuilding_InCamera : InCamera
	{
		[SerializeField] private TMP_Text nameText;
		[SerializeField] private TMP_Text storageText;
		[SerializeField] private ResourceIconButtonWidget resourceIconButtonWidget;

		private Vector3 startScale;


		public void OnBuildingHover()
		{
			this.transform.localScale *= 1.5f;
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

		internal void Init(Building building)
		{
			startScale = this.transform.localScale;
		}
	}
}