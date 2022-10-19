using System;
using UnityEngine;

public abstract class Building : InteractableObject
{
	[SerializeField]
	private string buildingName;

	[SerializeField]
	private Color buildingColor;

	[SerializeField]
	private Sprite buildingIcon;

	[SerializeField]
	public int buildCost = 100;//TODO temp

	private bool isFreeToUse = true;
	public IslandBlock islandBlock; //TODO set in buildingcontroller

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
