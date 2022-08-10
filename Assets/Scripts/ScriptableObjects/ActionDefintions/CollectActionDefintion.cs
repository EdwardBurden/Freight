using System;
using System.Collections;
using System.Collections.Generic;
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

	public override ActionPreview CreateActionPreview(Ship ship, Action<ShipAction> callback)
	{
		CollectActionPreview previewMoveAction = Instantiate(collectActionPreviewprefab);
		previewMoveAction.Open(ship, (producer, resource) => OnResourceSelected(ship, producer, resource, callback));
		return previewMoveAction;
	}
	public override void ActionSelected(Ship shipObject, InteractableObject interactableObject, Vector3 clickPosition, Action<ShipAction> callback)
	{
		IProducer producer = interactableObject.GetComponent<IProducer>();
		ResourceSelectScreen.Create(producer.GetProducedResources(), clickPosition, (resource) => OnResourceSelected(shipObject, producer, resource, callback), false);
	}

	private void OnResourceSelected(Ship shipObject, IProducer producer, ResourceDefintion resourceDefintion, Action<ShipAction> callback)
	{
		CollectAction collectAction = new CollectAction(this, shipObject, producer, resourceDefintion);
		callback?.Invoke(collectAction);
	}
}
