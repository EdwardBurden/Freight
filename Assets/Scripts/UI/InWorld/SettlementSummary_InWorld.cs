using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace UI
{
	public class SettlementSummary_InWorld : BuildingSummary_InWorld
	{
		[SerializeField] private RequiredCollapsedResourceWidget collapsedResourceWidget;
		[SerializeField] private Transform requestedResourcesTransform;

		private Settlement settlement => building as Settlement;


		public override void Show()
		{
			base.Show();
			buildingNameText.text = $"{building.GetInteractableObjectName()} {settlement.level}"; ;
			GameObjectUtils.Clear(requestedResourcesTransform);
			SettlementRequest settlementRequests = settlement.GetResourceRequirements();
			if (settlementRequests == null)
			{
				return;
			}
			foreach (ResourceRequest item in settlementRequests.requests)
			{
				RequiredCollapsedResourceWidget requiredCollapsedResource = Instantiate(collapsedResourceWidget, requestedResourcesTransform);
				int stored = building.storage.GetStorage(item.resourceDefintion);
				int required = item.amountRequired;
				requiredCollapsedResource.ShowResource(item.resourceDefintion, required, stored);
			}
		}

	}
}
