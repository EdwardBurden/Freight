using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;

public class SettlementPanel : BuildingPanel
{
	[SerializeField]
	private TMP_Text rewardField;

	[SerializeField]
	private Transform acceptsTransform;

	[SerializeField]
	private ResourceIconButtonWidget resourceIconButtonWidget;

	protected override void OnBuildingSelected(Building building)
	{
		if (building is Settlement settlement)
		{
			base.OnBuildingSelected(building);
			ShowResourcesRequired(settlement);
		}
	}

	public void ShowResourcesRequired(Settlement settlement)
	{
		GameObjectUtils.Clear(acceptsTransform);
		foreach (ResourceDefintion resource in settlement.GetAcceptedResources())
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
