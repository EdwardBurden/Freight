using System;
using UnityEngine;

public abstract class Building : InteractableObject
{
	[SerializeField]
	private GameObject inWorldHoverUI;
	[SerializeField]
	private string buildingName;

	[SerializeField]
	private Color buildingColor;

	[SerializeField]
	private Sprite buildingIcon;

	[SerializeField]
	public int buildCost = 100;//TODO temp

	private bool isFreeToUse = true;
	[HideInInspector]
	public IslandBlock islandBlock; //TODO set in buildingcontroller

	private void Start()
	{
		Instantiate(inWorldHoverUI, this.transform);
	}

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

	protected virtual void OnSeasonStart(SeasonType obj) { }


	protected virtual void OnSeasonEnd(SeasonType obj) { }


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
}
