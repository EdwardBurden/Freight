using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
namespace UI
{
	public class ExpandedResourceWidget : MonoBehaviour
	{
		[SerializeField] private TMP_Text resourceNameText;
		[SerializeField] private TMP_Text resourceAmountText;
		[SerializeField] private TMP_Text resourceBuyPriceText;
		[SerializeField] private TMP_Text resourceSellPriceText;
		[SerializeField] private SimpleButtonWidget simpleButtonWidget;

		public void ShowResource(ResourceDefintion resourceData, int amount)
		{
			resourceNameText.text = resourceData.resourceName;
			resourceAmountText.text = amount.ToString();
			resourceBuyPriceText.text = resourceData.buyPrice.ToString();
			resourceSellPriceText.text = resourceData.sellPrice.ToString();
			simpleButtonWidget.Init(resourceData.resourceColor, resourceData.resourceIcon, null, false);
		}
	}
}