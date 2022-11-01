using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;

public class FarmPanel : BuildingPanel
{
	[SerializeField]
	private TMP_Text producedperAutumn;
	[SerializeField]
	private TMP_Text lostperSeason;
	[SerializeField]
	private TMP_Text cropStored;

	[SerializeField]
	private Transform producesTransform;

	[SerializeField]
	private ResourceIconButtonWidget resourceIconButtonWidget;

	protected override void OnBuildingSelected(Building building)
	{
		if (building is Farm farm)
		{
			base.OnBuildingSelected(building);
			ShowResourceProduced(farm);
		}
	}


	private void ShowResourceProduced(Farm farm)//TODO add amounts and amountsLeft
	{
		GameObjectUtils.Clear(producesTransform);
		ResourceDefintion resourceDefintion = farm.GetProducedResources()[0];
		ResourceIconButtonWidget widget = Instantiate(resourceIconButtonWidget, producesTransform);
		widget.ShowResource(resourceDefintion, () => ShowResoucreDetail(resourceDefintion));

		cropStored.text = farm.cropStored.ToString();
	}

	public void ShowResoucreDetail(ResourceDefintion resource)
	{
		//TODO Open Screen
	}
}
