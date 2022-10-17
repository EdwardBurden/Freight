using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;

public class ConsumerPanel : Panel
{
	[SerializeField]
	private TMP_Text nameField;

	private Building building;


	[SerializeField]
	private Transform wantsTransform;

	[SerializeField]
	private TMP_Text rewardField;

	[SerializeField]
	private Transform acceptsTransform;

	[SerializeField]
	private ConsumerWantWidget consumerWantWidget;

	[SerializeField]
	private ResourceIconButtonWidget resourceIconButtonWidget;

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

	private void BuildingController_BuildingSelectedEvent(Building building)
	{
		Hide();
		if (building is IConsumer consumer)
		{
			Refresh(building, consumer);
			Show();
		}
	}

	public void Refresh(Building building, IConsumer consumer)
	{
		this.building = building;
		nameField.text = building.GetObjectName();

		GameObjectUtils.Clear(wantsTransform);
		GameObjectUtils.Clear(acceptsTransform);
		SettlementWants consumerWants = consumer.GetWants();
		foreach (ConsumerWant want in consumerWants.consumerWants)
		{
			AddConsumerWant(want);
		}
		rewardField.text = $"{consumerWants.rewardAmount} for Completing Wants";

		foreach (ResourceDefintion resource in consumer.GetAcceptedResources())
		{
			AddAccept(resource);
		}
	}

	private void AddAccept(ResourceDefintion resourceDefintion)
	{
		ResourceIconButtonWidget widget = Instantiate(resourceIconButtonWidget, acceptsTransform);
		widget.ShowResource(resourceDefintion);
	}


	private void AddConsumerWant(ConsumerWant consumerWant)
	{
		ConsumerWantWidget widget = Instantiate(consumerWantWidget, wantsTransform);
		widget.Init(consumerWant);
	}


}
