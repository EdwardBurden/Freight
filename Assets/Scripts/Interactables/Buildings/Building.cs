using System;
using UI;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Building : InteractableObject
{
	[SerializeField]
	private InWorld inWorldHoverUI;
	[SerializeField]
	private string buildingName;
	[SerializeField]
	private Color buildingColor;
	[SerializeField]
	private Sprite buildingIcon;
	[SerializeField]
	private Transform worldUITransform;
	[SerializeField]
	public int buildCost = 100;//TODO temp

	[HideInInspector]
	public IslandBlock islandBlock; //TODO set in buildingcontroller
	private bool isFreeToUse = true;
	protected InWorld inWorldUI;

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

	public virtual void OnBuildingPlaced()
	{
		if (inWorldHoverUI != null && worldUITransform != null)
		{
			inWorldUI = Instantiate(inWorldHoverUI, worldUITransform);
		}
		UpdateUI();
	}

	protected virtual void OnSeasonStart(SeasonType obj) { UpdateUI(); }


	protected virtual void OnSeasonEnd(SeasonType obj) { UpdateUI(); }


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

	protected virtual void UpdateUI() { }

	public override void OnHover()
	{
		base.OnHover();
		if (inWorldUI != null)
		{
			WellBuildingDisplay_InWorld wellInWorldUI = inWorldUI.GetComponent<WellBuildingDisplay_InWorld>();
			wellInWorldUI.OnBuildingHover();
		}
	}

	public override void OffHover()
	{
		base.OffHover();
		if (inWorldUI != null)
		{
			WellBuildingDisplay_InWorld wellInWorldUI = inWorldUI.GetComponent<WellBuildingDisplay_InWorld>();
			wellInWorldUI.OffBuildingHover();
		}
	}
}
