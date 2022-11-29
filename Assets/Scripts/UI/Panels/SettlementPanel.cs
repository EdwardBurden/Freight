using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;

public class SettlementPanel : BuildingPanel<Settlement>
{
	[SerializeField] private TMP_Text rewardField;
	[SerializeField] private Transform acceptsTransform;
	[SerializeField] private RequiredExpandedResourceWidget resourceIconButtonWidget;

	public override void Show()
	{
		base.Show();
		GameObjectUtils.Clear(acceptsTransform);
		SettlementRequestData settlementRequirements = building.GetResourceRequirements();
		if (settlementRequirements != null)
		{
			rewardField.text = settlementRequirements.creditReward.ToString();
			foreach (SettlementRequest settlement in settlementRequirements.requests)
			{
				ShowResourceRequired(settlement);
			}
		}
		else
		{
			rewardField.text = "Must wait till year start for requests";
		}
	}

	private void ShowResourceRequired(SettlementRequest settlementRequirement)//TODO add amounts and amountsLeft
	{
		int amountStored = buildingStorage.GetStorage(settlementRequirement.resourceDefintion);
		int amountRequired = settlementRequirement.amountRequired;
		RequiredExpandedResourceWidget widget = Instantiate(resourceIconButtonWidget, acceptsTransform);
		widget.ShowResource(settlementRequirement.resourceDefintion, amountRequired, amountStored);
	}
}
