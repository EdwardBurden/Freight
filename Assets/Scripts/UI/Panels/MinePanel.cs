using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;

public class MinePanel : BuildingPanel
{
	[SerializeField]
	private TMP_Text oreStored;

	[SerializeField]
	private Transform producesTransform;

	[SerializeField]
	private ResourceIconButtonWidget resourceIconButtonWidget;

	protected override void OnBuildingSelected(Building building)
	{
		if (building is Mine mine)
		{
			base.OnBuildingSelected(building);
			ShowResourceProduced(mine);
		}
	}


	private void ShowResourceProduced(Mine mine)//TODO add amounts and amountsLeft
	{
		GameObjectUtils.Clear(producesTransform);
		ResourceDefintion resourceDefintion = mine.GetProducedResources()[0];
		ResourceIconButtonWidget widget = Instantiate(resourceIconButtonWidget, producesTransform);
		widget.ShowResource(resourceDefintion, () => ShowResoucreDetail(resourceDefintion));

		oreStored.text = mine.storedOre.ToString();
	}

	public void ShowResoucreDetail(ResourceDefintion resource)
	{
		//TODO Open Screen
	}

}
