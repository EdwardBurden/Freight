using System;
using Terrain;
using UI;
using Unity.VisualScripting;
using UnityEngine;

public abstract class LevelBuilding : MonoBehaviour
{
	[SerializeField] protected Transform summaryUIAnchor;
	[SerializeField] public Storage storage;

	protected BuildingSummary_InWorld summaryUI;
	protected BuildingLevelDefintion buildingDefintion;
	private bool isFreeToUse = true; //TODO replace with a queue of ships 

	protected ResourceRequest[] needs; //can be a requestgroup , with helper methods inside

	protected Building buildingContainer;

	public virtual void Init(BuildingLevelDefintion buildingDefintion, Building building)
	{
		this.buildingContainer = building;
		this.buildingDefintion = buildingDefintion;
		if (buildingDefintion.summaryUIPrefab != null && summaryUIAnchor != null)
		{
			summaryUI = Instantiate(buildingDefintion.summaryUIPrefab, summaryUIAnchor);
			summaryUI.Init(this);
		}

		needs = buildingDefintion.CreateNeeds();
	}

	private void OnDestroy()
	{
		if (summaryUI != null)
		{
			Destroy(summaryUI.gameObject);
		}
	}

	public string GetInteractableObjectName()
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
}
