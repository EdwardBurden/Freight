using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{

    public class RequiredExpandedResourceWidget : MonoBehaviour
    {
		[SerializeField] private TMP_Text resourceStoredToRequiredText;
		[SerializeField] private TMP_Text resourceBuyPriceText;
		[SerializeField] private TMP_Text resourceSellPriceText;
		[SerializeField] private Image resourceIconImage;

		public virtual void ShowResource(ResourceRequest resourceRequest)
		{
			resourceStoredToRequiredText.text = $"{resourceRequest.amountFulfilled}/{resourceRequest.amount}";
			resourceBuyPriceText.text = resourceRequest.resource.buyPrice.ToString();
			resourceSellPriceText.text = resourceRequest.resource.sellPrice.ToString();
			resourceIconImage.sprite = resourceRequest.resource.resourceIcon;
		}

		public virtual void ShowResource(ResourceDefintion resourceData, int required, int stored)
		{
			resourceStoredToRequiredText.text = $"{stored}/{required}";
			resourceBuyPriceText.text = resourceData.buyPrice.ToString();
			resourceSellPriceText.text = resourceData.sellPrice.ToString();
			resourceIconImage.sprite = resourceData.resourceIcon;
		}
	}
}