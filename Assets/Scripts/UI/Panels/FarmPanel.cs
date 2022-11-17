using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;

public class FarmPanel : BuildingPanel<Farm>
{
	[SerializeField] private TMP_Text producedperAutumn;
	[SerializeField] private TMP_Text lostperSeason;
	[SerializeField] private TMP_Text cropStored;
	[SerializeField] private Transform producesTransform;
	[SerializeField] private ResourceIconButtonWidget resourceIconButtonWidget;

	public override void Show()
	{
		base.Show();
		GameObjectUtils.Clear(producesTransform);
		ResourceDefintion resourceDefintion = building.GetResourceProduced();
		ResourceIconButtonWidget widget = Instantiate(resourceIconButtonWidget, producesTransform);
		widget.ShowResource(resourceDefintion, () => ShowResoucreDetail(resourceDefintion));

		cropStored.text = building.GetCropsStoredAmount().ToString();
	}

	public void ShowResoucreDetail(ResourceDefintion resource)
	{
		//TODO Open Screen
	}
}
