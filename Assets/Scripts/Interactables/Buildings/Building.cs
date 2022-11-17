using System;
using UI;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Building : InteractableObject
{
	[SerializeField] protected Transform summaryUIAnchor;
	[SerializeField] public Storage storage;


	protected SummaryBuilding_InCamera summaryUI;
	protected BuildingDefintion buildingDefintion;
	[HideInInspector] public IslandBlock islandBlock;
	private bool isFreeToUse = true; //TODO replace with a queue of ships 

	public virtual void Init(BuildingDefintion buildingDefintion)
	{
		this.buildingDefintion = buildingDefintion;
		if (buildingDefintion.summaryUIPrefab != null && summaryUIAnchor != null)
		{
			summaryUI = GameUIController.instance.CreateInScene(buildingDefintion.summaryUIPrefab, summaryUIAnchor) as SummaryBuilding_InCamera;
			summaryUI.Init(this);
		}
	}


	private void OnDestroy()
	{
		if (summaryUI != null)
		{
			Destroy(summaryUI.gameObject);
		}
	}

	public override string GetObjectName()
	{
		return buildingDefintion.buildingName;
	}

	public bool IsFree()
	{
		return isFreeToUse;
	}

	public void SetBuildingFreedom(bool isFree)
	{
		isFreeToUse = isFree;
	}

	public Sprite GetBuildingIcon()
	{
		return buildingDefintion.buildingIcon;
	}

	public override void OnHover()
	{
		base.OnHover();
		if (summaryUI != null)
		{
			SummaryBuilding_InCamera wellInWorldUI = summaryUI.GetComponent<SummaryBuilding_InCamera>();
			wellInWorldUI.OnBuildingHover();
		}
	}

	public override void OffHover()
	{
		base.OffHover();
		if (summaryUI != null)
		{
			SummaryBuilding_InCamera wellInWorldUI = summaryUI.GetComponent<SummaryBuilding_InCamera>();
			wellInWorldUI.OffBuildingHover();
		}
	}
}
