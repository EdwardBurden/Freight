using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;

public class FarmPanel : BuildingPanel<ProducerLevelBuilding>
{
	[SerializeField] private TMP_Text producedperTurnText;
	[SerializeField] private Transform producesTransform;
	[SerializeField] private ExpandedResourceWidget expandedResourceWidget;

	public override void Show()
	{
		base.Show();
		GameObjectUtils.Clear(producesTransform);
		ResourceDefintion resourceDefintion = building.GetResourceProduced();
		ExpandedResourceWidget widget = Instantiate(expandedResourceWidget, producesTransform);
		widget.ShowResource(resourceDefintion, building.GetCropsStoredAmount());
		producedperTurnText.text = building.GetAmountProducedPerTurn().ToString();
	}
}
