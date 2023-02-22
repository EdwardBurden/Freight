using System;
using Terrain;
using UI;
using Unity.VisualScripting;
using UnityEngine;

public abstract class LevelBuilding : InteractableObject
{
	[SerializeField] protected Transform summaryUIAnchor;
	[SerializeField] public Storage storage;
	protected BuildingSummary summaryUI;
	protected BuildingLevelDefintion buildingDefintion;


	private bool isFreeToUse = true; //TODO replace with a queue of ships 
	public RequestGroup needs;
	public RequestGroup wants;//can be a requestgroup , with helper methods inside
	protected Building buildingContainer;

	public virtual void Init(BuildingLevelDefintion buildingDefintion, Building building)
	{
		this.buildingContainer = building;
		this.buildingDefintion = buildingDefintion;
		needs = new RequestGroup(buildingDefintion.needs);
		wants = new RequestGroup(buildingDefintion.wants);
		if (buildingDefintion.summaryUIPrefab != null && summaryUIAnchor != null)
		{
			summaryUI = Instantiate(buildingDefintion.summaryUIPrefab, summaryUIAnchor);
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

	public override string GetInteractableObjectName()
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
			summaryUI.OnBuildingHover();
		}
	}

	public override void OffHover()
	{
		base.OffHover();
		if (summaryUI != null)
		{
			summaryUI.OffBuildingHover();
		}
	}

	internal bool AreWantsFulfilled()
	{
		return false;
		throw new NotImplementedException();
	}

	internal bool AreNeedsFulfilled()
	{
		return false;
		throw new NotImplementedException();
	}
}
