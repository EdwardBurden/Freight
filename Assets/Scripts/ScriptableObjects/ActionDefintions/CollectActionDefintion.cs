using System;
using UI;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CollectActionDefintion")]
public class CollectActionDefintion : ShipActionDefintion
{
	[SerializeField]
	private CollectActionPreview collectActionPreviewprefab;

	public override bool CanBeCreated(Ship shipObject, InteractableObject interactableObject, Vector3 postion)
	{
		IProducer producer = interactableObject.GetComponent<IProducer>();
		CarryObject carryBehaviour = shipObject.GetComponent<CarryObject>();
		if (producer == null || carryBehaviour == null)
		{
			return false;
		}
		return true;
	}

	public override void CreateActionPreview(Ship ship, Action<ShipOrder> callback)
	{
		CollectActionPreview collectActionPreview = PreviewController.instance.CreateActionPreview(ship, collectActionPreviewprefab) as CollectActionPreview;
		collectActionPreview.Init(ship);
		collectActionPreview.callback = (producer, resource) => OnResourceSelected(ship, producer, resource, callback);
	}

	public override void ActionSelected(Ship shipObject, InteractableObject interactableObject, Vector3 clickPosition, Action<ShipOrder> callback)
	{
		IProducer producer = interactableObject.GetComponent<IProducer>();
		ResourceSelectScreen.Create(producer.GetProducedResources(), clickPosition, (resource) => OnResourceSelected(shipObject, producer, resource, callback), false);
	}

	private void OnResourceSelected(Ship shipObject, IProducer producer, ResourceDefintion resourceDefintion, Action<ShipOrder> callback)
	{
		CollectOrder collectAction = new CollectOrder(this, shipObject, producer, resourceDefintion);
		callback?.Invoke(collectAction);
	}
}
