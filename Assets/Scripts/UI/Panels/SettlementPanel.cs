using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;

public class SettlementPanel : BuildingPanel<Settlement>
{
	[SerializeField] private TMP_Text rewardField;
	[SerializeField] private Transform acceptsTransform;
	[SerializeField] private ResourceIconButtonWidget resourceIconButtonWidget;

	public override void Show()
	{
		base.Show();
		GameObjectUtils.Clear(acceptsTransform);
		foreach (ResourceDefintion resource in building.GetAcceptedResources())
		{
			ShowResourceRequired(resource);
		}
	}

	private void ShowResourceRequired(ResourceDefintion resource)//TODO add amounts and amountsLeft
	{
		ResourceIconButtonWidget widget = Instantiate(resourceIconButtonWidget, acceptsTransform);
		widget.ShowResource(resource, () => ShowResoucreDetail(resource));
	}

	public void ShowResoucreDetail(ResourceDefintion resource)
	{
		//TODO Open Screen
	}
}
