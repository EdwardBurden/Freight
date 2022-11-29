using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceProducedWidget : MonoBehaviour
{
	[SerializeField] private TMP_Text text;
	[SerializeField] private Image icon;

	public void ShowResource(ResourceDefintion resourceData, int stored)
	{
		text.text = stored.ToString();
		icon.sprite = resourceData.resourceIcon;
	}
}
