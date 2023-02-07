using System;
using System.Collections;
using System.Collections.Generic;
using Terrain;
using UI;
using UnityEngine;

public abstract class BuildingDefintion : ScriptableObject
{
	[SerializeField] public string buildingName;
	[SerializeField] public Sprite buildingIcon;
	[SerializeField] public int buildCost = 100;
	[SerializeField] public ResourceGroundType[] requiredGroundType;

	[SerializeField] public BuildingSummary_InWorld summaryUIPrefab; //summary above building

	[SerializeField] public Building buildingPrefab;

	public virtual bool CanBePlaced(IslandBlock islandBlock)
	{
		if (requiredGroundType.Length == 0)
		{
			return true;
		}

		for (int i = 0; i < requiredGroundType.Length; i++)
		{
			if (requiredGroundType[i] != islandBlock.resourceGroundType)
			{
				return false;
			}
		}
		return true;
	}
}
