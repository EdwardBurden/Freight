using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;

public class ConsumerPanel : BuildingPanel<ConsumerLevelBuilding>
{
	[SerializeField] private TMP_Text rewardField;
	[SerializeField] private Transform acceptsTransform;
	[SerializeField] private RequiredExpandedResourceWidget resourceIconButtonWidget;

	public override void Show()
	{
		base.Show();
		GameObjectUtils.Clear(acceptsTransform);
		if (building.needs == null || building.needs.resourceRequests == null)
		{
			return;
		}
		RequestGroup requestGroup = building.needs;
		foreach (ResourceRequest resourceRequest in requestGroup.resourceRequests)
		{
			ShowResourceRequired(resourceRequest);
		}

	}


	private void ShowResourceRequired(ResourceRequest request)//TODO add amounts and amountsLeft
	{
		RequiredExpandedResourceWidget widget = Instantiate(resourceIconButtonWidget, acceptsTransform);
		widget.ShowResource(request);
	}
}
