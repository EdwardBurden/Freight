using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : InteractableObject
{
	[SerializeField]
	private GameObject buildingUI;
	[SerializeField]
	private GameObject buildingMenuPrefab;

	protected override void Awake()
	{
		base.Awake();
		buildingUI.SetActive(false);
	}

	protected override void OffHover(InteractableObject obj)
	{
		if (obj == this)
		{
			buildingUI.SetActive(false);

			//this.transform.localScale /= 0.9f;
		}
	}

	protected override void OnHover(InteractableObject obj)
	{
		if (obj == this)
		{
			buildingUI.SetActive(true);
			//this.transform.localScale *= 0.9f;
		}
	}

	public virtual BuildingMenu CreateBuildingMenu(Transform canvas)
	{
		return Instantiate(buildingMenuPrefab, canvas).GetComponent<BuildingMenu>();
	}
}
