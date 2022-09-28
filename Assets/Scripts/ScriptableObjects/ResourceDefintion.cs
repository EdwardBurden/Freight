using System;
using UI;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/ResourceData")]
public class ResourceDefintion : ScriptableObject
{
	public string resourceName;
	public Sprite resourceIcon;
	public Color resourceColor;
	public ResourceIconButtonWidget iconPrefab;
	public FreightObject freightPrefab;
	public int price;
	public ResourceCategory resourceCategory;

	public ResourceIconButtonWidget CreateResourceSelectionButton(Transform options, Action callback)
	{
		ResourceIconButtonWidget shipActionIconWidget = Instantiate(iconPrefab, options);
		shipActionIconWidget.ShowResource(this, callback);
		return shipActionIconWidget;
	}
}
