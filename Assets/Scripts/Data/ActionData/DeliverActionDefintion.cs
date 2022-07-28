using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
//TODO
[CreateAssetMenu(menuName = "ScriptableObject/DeliverActionDefintion")]
public class DeliverActionDefintion : ShipActionDefintion
{
	[SerializeField]
	private DeliverActionPreview  deliverActionPreviewPrefab;
	public override bool CanShowOption(RightClickContextActionInfo shipActionSelectionInfo)
	{
		IConsumer consumer = shipActionSelectionInfo.interactable.GetComponent<IConsumer>();
		CarryObject carryBehaviour = shipActionSelectionInfo.ship.gameObject.GetComponent<CarryObject>();
		if (consumer == null || carryBehaviour == null)
		{
			return false;
		}
		return true;
	}

	public override void OnOptionSelectedFromActionsListPanel(ShipObject ship, Action<ShipAction> callback)
	{
		DeliverActionPreview  deliverActionPreview = Instantiate(deliverActionPreviewPrefab);
		deliverActionPreview.Open(ship, (consumer, position) => OnPreviewClick(ship, consumer, position, callback));
	}
	private void OnPreviewClick(ShipObject ship, IConsumer consumer, Vector3 clickPosition, Action<ShipAction> callback)
	{
		ResourceSelectScreen.Create(consumer.GetAcceptedResources(), clickPosition, (resource) => OnResourceSelected(ship, consumer, resource, callback), false);
	}

	public override void OnDefaultRightClickMenuOption(RightClickContextActionInfo shipActionSelectionInfo, Action<ShipAction> callback)
	{
		IConsumer consumer = shipActionSelectionInfo.interactable.GetComponent<IConsumer>();
		ResourceSelectScreen.Create(consumer.GetAcceptedResources(), shipActionSelectionInfo.clickPosition, (resource) => OnResourceSelected(shipActionSelectionInfo, resource, callback) ,true);
	}

	public override void OnOptionSelectedFromRightClickMenu(RightClickContextActionInfo shipActionSelectionInfo, Action<ShipAction> callback)
	{
		IConsumer consumer = shipActionSelectionInfo.interactable.GetComponent<IConsumer>();
		ResourceSelectScreen.Create(consumer.GetAcceptedResources(), shipActionSelectionInfo.clickPosition, (resource) => OnResourceSelected(shipActionSelectionInfo, resource, callback),false);
	}
	private void OnResourceSelected(ShipObject shipObject, IConsumer  consumer, ResourceDefintion resourceDefintion, Action<ShipAction> callback)
	{
		DeliverAction deliverAction = new DeliverAction(this, shipObject, consumer, resourceDefintion);
		callback?.Invoke(deliverAction);
	}
	private void OnResourceSelected(RightClickContextActionInfo shipActionSelectionInfo, ResourceDefintion resourceData, Action<ShipAction> callback)
	{
		IConsumer consumer = shipActionSelectionInfo.interactable.GetComponent<IConsumer>();
		DeliverAction deliverAction = new DeliverAction(this, shipActionSelectionInfo.ship, consumer, resourceData);
		callback?.Invoke(deliverAction);
	}
}
