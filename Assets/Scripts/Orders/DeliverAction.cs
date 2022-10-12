using ActionLists;
using UnityEngine;

public class DeliverAction : ShipOrder
{
	private CarryObject carryObject;
	private IConsumer consumer;
	private ResourceDefintion resourceData;

	public DeliverAction(ShipActionDefintion shipActionDefintion, Ship shipObject, IConsumer target, ResourceDefintion resourceTarget) : base(shipActionDefintion, shipObject)
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
		Building building = (Building)consumer; ;

		//Move
		childList.PushAction(new LookAtAction(position, shipObject, true));
		childList.PushAction(new AlignShipForDeliveryAction(shipObject, carryObject, resourceData, landingPlatform));
		childList.PushAction(new WaitForBuildingTobeFreeAction(building));

		//Deliver
		childList.PushAction(new SetBuildingFreedomAction(building, false));
		childList.PushAction(new AnimateLoadingPlatformOpenAction(landingPlatform));
		childList.PushAction(new ParentFreightToPlatformAction(landingPlatform, carryObject, resourceData));
		childList.PushAction(new AnimateLoadingPlatformCloseAction(landingPlatform));
		childList.PushAction(new SetBuildingFreedomAction(building, true));	
		childList.PushAction(new DestoryFreightAction(landingPlatform));
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

	public override void Finish()
	{
		WalletController wallet = GameObject.FindObjectOfType<WalletController>();
		wallet.CreateAndAddSellTransaction(resourceData.sellPrice, consumer as Building, shipObject, resourceData);
	}
}
