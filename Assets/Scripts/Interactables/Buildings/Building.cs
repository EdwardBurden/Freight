using System;
using UI;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Building : InteractableObject
{
	//summaryUI
	[SerializeField] private SummaryBuilding_InCamera summaryUIPrefab;
	[SerializeField] private Transform summaryUIAnchor;
	private SummaryBuilding_InCamera summaryUI;

	//Data
	[SerializeField] private string buildingName;
	[SerializeField] private Color buildingColor;
	[SerializeField] private Sprite buildingIcon;
	[SerializeField] public int buildCost = 100;

	[HideInInspector] public IslandBlock islandBlock;
	private bool isFreeToUse = true; //TODO replace with a queue of ships 

	private void OnEnable()
	{
		SeasonsController.SeasonStartedEvent += OnSeasonStart;
		SeasonsController.SeasonEndedEvent += OnSeasonEnd;
	}

	private void OnDisable()
	{
		SeasonsController.SeasonStartedEvent -= OnSeasonStart;
		SeasonsController.SeasonEndedEvent -= OnSeasonEnd;
	}

	private void OnDestroy()
	{
		if (summaryUI != null)
		{
			//un hook event
			//destory ui
		}
	}

	public virtual void OnBuildingPlaced()
	{
		if (summaryUIPrefab != null && summaryUIAnchor != null)
		{
			summaryUI = GameUIController.instance.CreateInScene(summaryUIPrefab, summaryUIAnchor) as SummaryBuilding_InCamera;
			summaryUI.Init(this);
			//Hook up events
		}
		UpdateSummaryUI();
	}

	protected virtual void OnSeasonStart(SeasonType obj)
	{
		UpdateSummaryUI();
	}


	protected virtual void OnSeasonEnd(SeasonType obj)
	{
		UpdateSummaryUI();
	}


	public override string GetObjectName()
	{
		return buildingName;
	}

	public bool IsFree()
	{
		return isFreeToUse;
	}

	public void SetBuildingFreedom(bool isFree)
	{
		isFreeToUse = isFree;
	}

	public Color GetBuildingColor()
	{
		return buildingColor;
	}

	public Sprite GetBuildingIcon()
	{
		return buildingIcon;
	}

	protected virtual void UpdateSummaryUI()
	{
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
