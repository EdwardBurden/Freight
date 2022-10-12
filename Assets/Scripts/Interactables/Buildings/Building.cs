using System;
using UnityEngine;

public abstract class Building : InteractableObject
{
	[SerializeField]
	private string buildingName;

	private bool isFreeToUse = true;

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
}
