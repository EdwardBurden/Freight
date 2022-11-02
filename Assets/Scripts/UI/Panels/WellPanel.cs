using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;

public class WellPanel : BuildingPanel
{
	[SerializeField]
	private TMP_Text waterStored;

	[SerializeField]
	private Transform producesTransform;

	[SerializeField]
	private ExpandedResourceWidget expandedResourceWidget;

	protected override void OnBuildingSelected(Building building)
	{
		if (building is Well well)
		{
			base.OnBuildingSelected(building);
			ShowResourceProduced(well);
		}
	}

	private void ShowResourceProduced(Well well)//TODO add amounts and amountsLeft
	{
		GameObjectUtils.Clear(producesTransform);
		ResourceDefintion resourceDefintion = well.GetProducedResources()[0];
		ExpandedResourceWidget widget = Instantiate(expandedResourceWidget, producesTransform);
		widget.ShowResource(resourceDefintion, well.waterStored);

		waterStored.text = well.waterStored.ToString();
	}

	public void ShowResoucreDetail(ResourceDefintion resource)
	{
		//TODO Open Screen
	}
}
