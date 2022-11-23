using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;

public class FarmPanel : BuildingPanel<Farm>
{
	[SerializeField] private TMP_Text producedperAutumn;
	[SerializeField] private TMP_Text lostperSeason;
	[SerializeField] private Transform producesTransform;
	[SerializeField] private ExpandedResourceWidget expandedResourceWidget;

	public override void Show()
	{
		base.Show();
		GameObjectUtils.Clear(producesTransform);
		ResourceDefintion resourceDefintion = building.GetResourceProduced();
		ExpandedResourceWidget widget = Instantiate(expandedResourceWidget, producesTransform);
		widget.ShowResource(resourceDefintion, building.GetCropsStoredAmount());

		producedperAutumn.text = building.GetAmountProducedPerAutumn().ToString();
		lostperSeason.text = building.GetAmountLostPerSeason().ToString();
	}
}
