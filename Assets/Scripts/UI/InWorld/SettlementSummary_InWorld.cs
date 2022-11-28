using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SettlementSummary_InWorld : BuildingSummary_InWorld
{
	[SerializeField] private RequiredCollapsedResourceWidget collapsedResourceWidget;
	[SerializeField] private Transform requestedResourcesTransform;

	private Settlement settlement => building as Settlement;


	protected override void Show()
	{
		base.Show();

		GameObjectUtils.Clear(requestedResourcesTransform);
		SettlementRequests settlementRequests = settlement.GetResourceRequirements();
		if (settlementRequests == null)
		{
			return;
		}
		foreach (SettlementRequest item in settlementRequests.requests)
		{
			Instantiate(collapsedResourceWidget, requestedResourcesTransform);
		}
	}
}
