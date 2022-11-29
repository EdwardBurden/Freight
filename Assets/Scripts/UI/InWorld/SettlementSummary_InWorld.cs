using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SettlementSummary_InWorld : BuildingSummary_InWorld
{
	[SerializeField] private RequiredCollapsedResourceWidget collapsedResourceWidget;
	[SerializeField] private Transform requestedResourcesTransform;

	private Settlement settlement => building as Settlement;


	public override void Show()
	{
		base.Show();

		GameObjectUtils.Clear(requestedResourcesTransform);
		SettlementRequestData settlementRequests = settlement.GetResourceRequirements();
		if (settlementRequests == null)
		{
			return;
		}
		foreach (SettlementRequest item in settlementRequests.requests)
		{
			RequiredCollapsedResourceWidget requiredCollapsedResource = Instantiate(collapsedResourceWidget, requestedResourcesTransform);
			int stored = building.storage.GetStorage(item.resourceDefintion);
			int required = item.amountRequired;
			requiredCollapsedResource.ShowResource(item.resourceDefintion, required, stored);
		}
	}


}
