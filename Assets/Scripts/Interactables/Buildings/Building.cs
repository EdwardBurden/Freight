using System;
using UI;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Building : InteractableObject
{
	[SerializeField] protected Transform summaryUIAnchor;
	[SerializeField] public Storage storage;


	protected BuildingSummary_InWorld summaryUI;
	protected BuildingDefintion buildingDefintion;
	[HideInInspector] public IslandBlock islandBlock;
	private bool isFreeToUse = true; //TODO replace with a queue of ships 

	public virtual void Init(BuildingDefintion buildingDefintion)
	{
		this.buildingDefintion = buildingDefintion;
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
			summaryUI.OnHover();
		}
	}

	public override void OffHover()
	{
		base.OffHover();
		if (summaryUI != null)
		{
			summaryUI.OffHover();
		}
	}
}
