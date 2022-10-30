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
	private ResourceIconButtonWidget resourceIconButtonWidget;

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
		ResourceIconButtonWidget widget = Instantiate(resourceIconButtonWidget, producesTransform);
		widget.ShowResource(resourceDefintion, () => ShowResoucreDetail(resourceDefintion));

		waterStored.text = well.waterStored.ToString();
	}

	public void ShowResoucreDetail(ResourceDefintion resource)
	{
		//TODO Open Screen
	}
}
