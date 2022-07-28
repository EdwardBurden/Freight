using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/CollectActionDefintion")]

//TODO
public class CollectActionDefintion : ShipActionDefintion
{
	[SerializeField]
	private CollectActionPreview collectActionPreviewprefab;
	public override bool CanShowOption(RightClickContextActionInfo shipActionSelectionInfo)
	{
		IProducer producer = shipActionSelectionInfo.interactable.GetComponent<IProducer>();
		CarryObject carryBehaviour = shipActionSelectionInfo.ship.gameObject.GetComponent<CarryObject>();
		if (producer == null || carryBehaviour == null)
		{
			return false;
		}
		return true;
	}

	public override ActionPreview CreateActionPreview(ShipObject ship, Action<ShipAction> callback)
	{
		CollectActionPreview previewMoveAction = Instantiate(collectActionPreviewprefab);
		previewMoveAction.Open(ship, (producer, resource) => OnResourceSelected(ship, producer, resource, callback));
		return previewMoveAction;
	}

	public override void OnDefaultRightClickMenuOption(RightClickContextActionInfo shipActionSelectionInfo, Action<ShipAction> callback)
	{
		IProducer producer = shipActionSelectionInfo.interactable.GetComponent<IProducer>();
		ResourceSelectScreen.Create(producer.GetProducedResources(), shipActionSelectionInfo.clickPosition, (resource) => OnResourceSelected(shipActionSelectionInfo, resource, callback), true);
	}

	public override void OnOptionSelectedFromRightClickMenu(RightClickContextActionInfo shipActionSelectionInfo, Action<ShipAction> callback)
	{
		IProducer producer = shipActionSelectionInfo.interactable.GetComponent<IProducer>();
		ResourceSelectScreen.Create(producer.GetProducedResources(), shipActionSelectionInfo.clickPosition, (resource) => OnResourceSelected(shipActionSelectionInfo, resource, callback), false);
	}

	private void OnResourceSelected(ShipObject shipObject, IProducer producer, ResourceDefintion resourceDefintion, Action<ShipAction> callback)
	{
		CollectAction collectAction = new CollectAction(this, shipObject, producer, resourceDefintion);
		callback?.Invoke(collectAction);
	}

	private void OnResourceSelected(RightClickContextActionInfo shipActionSelectionInfo, ResourceDefintion resourceData, Action<ShipAction> callback)
	{
		IProducer producer = shipActionSelectionInfo.interactable.GetComponent<IProducer>();
		CollectAction collectAction = new CollectAction(this, shipActionSelectionInfo.ship, producer, resourceData);
		callback?.Invoke(collectAction);
	}
}
