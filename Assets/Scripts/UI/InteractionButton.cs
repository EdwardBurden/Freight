using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionButton : MonoBehaviour
{
    [SerializeField]
    private Image icon;
    [SerializeField]
    private TMP_Text text;

	public void Init(string actionName, Sprite sprite)
	{
		text.text = actionName;
		icon.sprite = sprite;
	}
}
