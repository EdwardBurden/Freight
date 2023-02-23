using System;
using Terrain;
using UI;
using Unity.VisualScripting;
using UnityEngine;

//gets replaced, dont reference anywhere else!
public class LevelBuilding : MonoBehaviour //gets spawned but does nothing, pure visuals
{
	protected BuildingLevelDefintion buildingDefintion;
	public RequestGroup needs;
	public RequestGroup wants;//can be a requestgroup , with helper methods inside
	public Building buildingContainer;

	[SerializeField] protected Transform summaryUIAnchor;
	protected BuildingSummary summaryUI;

	public virtual void Init(BuildingLevelDefintion buildingDefintion, Building building)
	{
		this.buildingContainer = building;
		this.buildingDefintion = buildingDefintion;
		needs = new RequestGroup(buildingDefintion.needs);
		wants = new RequestGroup(buildingDefintion.wants);
		if (buildingDefintion.summaryUIPrefab != null && summaryUIAnchor != null)
		{
			summaryUI = Instantiate(buildingDefintion.summaryUIPrefab, summaryUIAnchor);
			summaryUI.Init(building);
		}
	}

	private void OnDestroy()
	{
		if (summaryUI != null)
		{
			Destroy(summaryUI.gameObject);
		}
	}

	public Sprite GetBuildingIcon()
	{
		return buildingDefintion.buildingIcon;
	}

	public void OnHover()
	{
		if (summaryUI != null)
		{
			summaryUI.OnBuildingHover();
		}
	}

	public void OffHover()
	{
		if (summaryUI != null)
		{
			summaryUI.OffBuildingHover();
		}
	}

	public bool AreWantsFulfilled()
	{
		return wants.AreAllRequestsFulfilled();
	}

	public bool AreNeedsFulfilled()
	{
		return needs.AreAllRequestsFulfilled();
	}

	internal bool HasWants()
	{
		return wants == null || wants.resourceRequests == null || wants.resourceRequests.Length == 0;
	}

	internal string GetLevelName()
	{
		return buildingDefintion.buildingName;
	}
}
