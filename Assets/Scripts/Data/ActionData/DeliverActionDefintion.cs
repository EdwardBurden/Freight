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
	private DeliverActionPreview deliverActionPreviewPrefab;

	public override bool CanBeCreated(ShipObject shipObject, InteractableObject interactableObject, Vector3 postion)
	{
		IConsumer consumer = interactableObject.GetComponent<IConsumer>();
		CarryObject carryBehaviour = shipObject.GetComponent<CarryObject>();
		if (consumer == null || carryBehaviour == null)
		{
			return false;
		}
		return true;
	}

	public override ActionPreview CreateActionPreview(ShipObject ship, Action<ShipAction> callback)
	{
		DeliverActionPreview deliverActionPreview = Instantiate(deliverActionPreviewPrefab);
		deliverActionPreview.Open(ship, (consumer, resource) => OnResourceSelected(ship, consumer, resource, callback));
		return deliverActionPreview;
	}

	public override void ActionSelected(ShipObject shipObject, InteractableObject interactableObject, Vector3 clickPosition, Action<ShipAction> callback)
	{
		IConsumer consumer = interactableObject.GetComponent<IConsumer>();
		ResourceSelectScreen.Create(consumer.GetAcceptedResources(), clickPosition, (resource) => OnResourceSelected(shipObject, consumer, resource, callback), true);
	}

	private void OnResourceSelected(ShipObject shipObject, IConsumer consumer, ResourceDefintion resourceDefintion, Action<ShipAction> callback)
	{
		DeliverAction deliverAction = new DeliverAction(this, shipObject, consumer, resourceDefintion);
		callback?.Invoke(deliverAction);
	}

}
