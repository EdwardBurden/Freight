using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RequiredCollapsedResourceWidget : MonoBehaviour
{
	[SerializeField] private TMP_Text resourceStoredToRequiredText;
	[SerializeField] private Image resourceIconImage;

	public virtual void ShowResource(ResourceDefintion resourceData, int required, int stored)
	{
		resourceStoredToRequiredText.text = $"{stored}/{required}";
		resourceIconImage.sprite = resourceData.resourceIcon;
	}
}
