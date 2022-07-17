using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleButtonWidget : MonoBehaviour
{
	[SerializeField]
	private Button button;

	[SerializeField]
	private Image background;

	[SerializeField]
	private Image icon;

	public void Init(Color color, Sprite sprite, Action callback)
	{
		this.icon.sprite = sprite;
		background.color = color;
		button.onClick.RemoveAllListeners();
		button.onClick.AddListener(() => callback());
	}

	public void Init(Action callback)
	{
		button.onClick.RemoveAllListeners();
		button.onClick.AddListener(() => callback());
	}
}
