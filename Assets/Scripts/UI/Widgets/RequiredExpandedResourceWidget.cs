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

		[SerializeField] private TMP_Text resourceNameText;
		[SerializeField] private TMP_Text resourceBuyPriceText;
		[SerializeField] private TMP_Text resourceSellPriceText;
		[SerializeField] private Image resourceIconImage;

		public virtual void ShowResource(ResourceDefintion resourceData, int required, int stored)
		{
			resourceStoredToRequiredText.text = $"{stored}/{required}";
			resourceBuyPriceText.text = resourceData.buyPrice.ToString();
			resourceSellPriceText.text = resourceData.sellPrice.ToString();
			resourceIconImage.sprite = resourceData.resourceIcon;
		}

		public void OnHover()
		{
			//TODO show resourceDefintion
		}
	}
}