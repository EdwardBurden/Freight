using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class WellPanel : BuildingPanel
{
	[SerializeField] private Transform producesTransform;
	[SerializeField] private ExpandedResourceWidget expandedResourceWidget;
	[SerializeField] private Transform producedPerSeasons;

	private Well well;

	protected override void OnBuildingSelected(Building building)
	{
		if (building is Well well)
		{
			Init(well);
			well.WaterStoredChangedEvent += Well_WaterStoredChangedEvent;
			base.OnBuildingSelected(building);
		}
	}

	private void Well_WaterStoredChangedEvent(int obj)
	{
		Show(); //temp
	}

	protected override void OnBuildingDeSelected(Building building)
	{
		if (well != null && building != well)
		{
			base.OnBuildingDeSelected(building);
			well.WaterStoredChangedEvent -= Well_WaterStoredChangedEvent;
		}
	}

	private void Init(Well well)
	{
		this.well = well;
	}

	public override void Show()
	{
		base.Show();
		ResourceDefintion resourceDefintion = well.GetProducedResources()[0];
		GameObjectUtils.Clear(producesTransform);
		ExpandedResourceWidget widget = Instantiate(expandedResourceWidget, producesTransform);
		widget.ShowResource(resourceDefintion, well.waterStored);

		for (int i = 0; i < well.amountProducedPerSeason.Length; i++)
		{
			producedPerSeasons.GetChild(i).GetComponent<TMP_Text>().text = ((SeasonType)i).ToString() + " " + well.amountProducedPerSeason[i].ToString();
		}
	}
}
