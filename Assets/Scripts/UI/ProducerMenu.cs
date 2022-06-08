using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProducerMenu : BuildingMenu
{

	public Transform resourceList;
	public Transform productionList;
	public GameObject resourceIconPrefab;

	private Producer producer => building as Producer;

	public override void Display(Building building)
	{
		base.Display(building);
		Producer producer = building as Producer;


		foreach (ResourceDefintion resourceDef in producer.resourcesProduced)
		{
			GameObject gameObject = Instantiate(resourceIconPrefab, resourceList);
			gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = resourceDef.icon;
			gameObject.GetComponent<Button>().onClick.AddListener(() => producer.AddOrder(resourceDef));
		}
	}

	private void Update()
	{
		foreach (Transform child in productionList)
		{
			GameObject.Destroy(child.gameObject);
		}
		foreach (var item in producer.productionQueue)
		{
			GameObject gameObject = Instantiate(resourceIconPrefab, productionList);
			gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = item.resource.icon;
			//gameObject.GetComponent<Button>().onClick.AddListener(() => producer.AddOrder(resourceDef));
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
