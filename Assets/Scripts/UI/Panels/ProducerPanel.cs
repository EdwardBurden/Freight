using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;

public class ProducerPanel : BuildingPanel<ProducerLevelBuilding>
{
	[SerializeField] private TMP_Text producedperTurnText;
	[SerializeField] private Transform producesTransform;
	[SerializeField] private RequiredExpandedResourceWidget resourceIconButtonWidget;

	public override void Show()
	{
		base.Show();
		GameObjectUtils.Clear(producesTransform);
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
		RequiredExpandedResourceWidget widget = Instantiate(resourceIconButtonWidget, producesTransform);
		widget.ShowResource(request);
	}
}
