using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumerMenu : BuildingMenu
{
	public Transform resourceList;
	public GameObject resourceIconPrefab;

	private Consumer consumer => building as Consumer;

	public override void Display(Building building)
	{
		base.Display(building);
		foreach (ResourceDefintion resourceDef in consumer.resourcesAccepted)
		{
			GameObject gameObject = Instantiate(resourceIconPrefab, resourceList);
			gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = resourceDef.icon;
		}
	}

	protected override void Clear()
	{
		foreach (Transform child in resourceList)
		{
			GameObject.Destroy(child.gameObject);
		}
	}
}