using System;
using UI;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/DeliverActionDefintion")]
public class DeliverActionDefintion : ShipActionDefintion
{
	[SerializeField]
	private DeliverActionPreview deliverActionPreviewPrefab;

	public override bool CanBeCreated(Ship shipObject, InteractableObject interactableObject, Vector3 postion)
	{
		IConsumer consumer = interactableObject.GetComponent<IConsumer>();
		CarryObject carryBehaviour = shipObject.GetComponent<CarryObject>();
		if (consumer == null || carryBehaviour == null)
		{
			return false;
		}
		return true;
	}

	public override void CreateActionPreview(Ship ship, Action<ShipOrder> callback)
	{
		DeliverActionPreview deliverActionPreview = Instantiate(deliverActionPreviewPrefab);
		PreviewController.instance.SetPreview(deliverActionPreviewPrefab);
		deliverActionPreview.Init(ship);
		deliverActionPreview.callback = (consumer, resource) => OnResourceSelected(ship, consumer, resource, callback);
	}

	public override void ActionSelected(Ship shipObject, InteractableObject interactableObject, Vector3 clickPosition, Action<ShipOrder> callback)
	{
		IConsumer consumer = interactableObject.GetComponent<IConsumer>();
		ResourceSelectScreen.Create(consumer.GetAcceptedResources(), clickPosition, (resource) => OnResourceSelected(shipObject, consumer, resource, callback), false);
	}

	private void OnResourceSelected(Ship shipObject, IConsumer consumer, ResourceDefintion resourceDefintion, Action<ShipOrder> callback)
	{
		DeliverOrder deliverAction = new DeliverOrder(this, shipObject, consumer, resourceDefintion);
		callback?.Invoke(deliverAction);
	}

}
