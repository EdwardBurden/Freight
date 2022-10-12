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
	public int buyPrice;
	public int sellPrice;

	public ResourceIconButtonWidget CreateResourceSelectionButton(Transform options, Action callback)
	{
		//can do something specific with icon if wanted. otherwise move to ui
		ResourceIconButtonWidget shipActionIconWidget = Instantiate(iconPrefab, options);
		shipActionIconWidget.ShowResource(this, callback);
		return shipActionIconWidget;
	}

	public FreightObject CreateFreight(Transform transform)
	{
		FreightObject freight = GameObject.Instantiate(freightPrefab, transform) as FreightObject;
		freight.resourceData = this;
		return freight;
	}
}
