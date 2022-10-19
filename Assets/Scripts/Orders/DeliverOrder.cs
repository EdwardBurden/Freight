using ActionLists;
using Unity.VisualScripting;
using UnityEngine;

public class DeliverOrder : ShipOrder
{
	private CarryObject carryObject;
	private IConsumer consumer;
	private ResourceDefintion resourceData;

	public DeliverOrder(ShipActionDefintion shipActionDefintion, Ship shipObject, IConsumer target, ResourceDefintion resourceTarget) : base(shipActionDefintion, shipObject)
	{
		this.carryObject = shipObject.GetComponent<CarryObject>();
		this.consumer = target;
		this.resourceData = resourceTarget;
	}

	public override void Setup()
	{
		base.Setup();
		LandingPlatform landingPlatform = consumer.GetConsumerLandingPlatform();
		Vector3 position = landingPlatform.GetLandingPosition();
		Building building = (Building)consumer;

		//Move
		childList.PushAction(new LookAtAction(position, shipObject, true));
		childList.PushAction(new AlignShipForDeliveryAction(shipObject, carryObject, resourceData, landingPlatform));

		//Wait
		childList.PushAction(new WaitForBuildingToBeFreeAction(building));
		childList.PushAction(new SetBuildingFreedomAction(building, false));

		//Deliver
		childList.PushAction(new AnimateLoadingPlatformOpenAction(landingPlatform));
		childList.PushAction(new ParentFreightToPlatformAction(landingPlatform, carryObject, resourceData));
		childList.PushAction(new ShipSellAction(consumer, resourceData, shipObject));
		childList.PushAction(new AnimateLoadingPlatformCloseAction(landingPlatform));
		childList.PushAction(new DestoryFreightAction(landingPlatform));

		//Sync
		childList.PushAction(new SetBuildingFreedomAction(building, true));
		childList.PushAction(new SyncAction());
	}

	public override void Start()
	{
		base.Start();
		if (!carryObject.HasFreightOfResource(resourceData))
		{
			RaiseFailEvent($"Cannot start Delivery because {shipObject.name} does not have any {resourceData.resourceName}");
		}
		if (!consumer.AcceptsResource(resourceData))
		{
			RaiseFailEvent($"Cannot start Delivery because {consumer.GetType().ToString()} does accept {resourceData.resourceName}");
		}
	}
}
