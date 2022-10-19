using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;

//TODO remove and replace with implomentation per building
public class ProducerPanel : Panel
{
	[SerializeField] private TMP_Text nameField;
	[SerializeField] private Transform producesTransfrom;
	[SerializeField] private ResourceIconButtonWidget resourceIconButtonWidget;

	private Building building;

	protected void OnEnable()
	{
		BuildingController.BuildingSelectedEvent += BuildingController_BuildingSelectedEvent;
		BuildingController.BuildingDeSelectedEvent += BuildingController_BuildingDeSelectedEvent;
	}

	private void OnDisable()
	{
		BuildingController.BuildingSelectedEvent -= BuildingController_BuildingSelectedEvent;
		BuildingController.BuildingDeSelectedEvent -= BuildingController_BuildingDeSelectedEvent;
	}

	private void BuildingController_BuildingDeSelectedEvent(Building obj)
	{
		Hide();
	}

	private void BuildingController_BuildingSelectedEvent(Building obj)
	{
		Hide();
		if (obj is IProducer producer)
		{
			Refresh(obj, producer);
			Show();
		}
	}

	public void Refresh(Building building, IProducer producer)
	{
		this.building = building;
		nameField.text = building.GetObjectName();

		GameObjectUtils.Clear(producesTransfrom);
		foreach (ResourceDefintion resource in producer.GetProducedResources())
		{
			AddProduce(resource);
		}
	}

	private void AddProduce(ResourceDefintion resourceDefintion)
	{
		ResourceIconButtonWidget widget = Instantiate(resourceIconButtonWidget, producesTransfrom);
		widget.ShowResource(resourceDefintion);
	}
}
