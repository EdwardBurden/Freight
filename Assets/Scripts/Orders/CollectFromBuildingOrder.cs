using ActionLists;
using UnityEngine;

//TOD make seperate order for collecting from non buildings
public class CollectFromBuildingOrder : ShipOrder
{
	private CarryObject carryObject;
	private ResourceDefintion resourceData;
	private IProducer producer;


	public CollectFromBuildingOrder(ShipActionDefintion shipActionDefintion, Ship shipObject, IProducer producer, ResourceDefintion resourceTarget) : base(shipActionDefintion, shipObject)
	{
		this.producer = producer; //hate but will come back to later
		this.carryObject = shipObject.GetComponent<CarryObject>();
		this.resourceData = resourceTarget;
	}

	public override void Setup()
	{
		base.Setup();
		Building building = (Building)producer;
		LandingPlatform landingPlatform = producer.GetProducerLandingPlatform();
		Transform navtransform = producer.GetProducerNavigationPostion();
		Vector3 position = landingPlatform.GetLandingPosition();

		//Move
		childList.PushAction(new LookAtAction(position, shipObject, true));
		childList.PushAction(new NavigateToAction(navtransform.position, shipObject, true));
		childList.PushAction(new AlignShipForCollectionAction(shipObject, carryObject, landingPlatform));

		//wait
		childList.PushAction(new WaitForBuildingToBeFreeAction(building));
		childList.PushAction(new SetBuildingFreedomAction(building, false));

		//Collect
		childList.PushAction(new SpawnFreightAction(resourceData, landingPlatform));
		childList.PushAction(new AnimateLoadingPlatformOpenAction(landingPlatform));
		childList.PushAction(new ShipBuyAction(producer, resourceData, shipObject));
		childList.PushAction(new ParentFreightToShipAction(landingPlatform, carryObject));
		childList.PushAction(new AnimateLoadingPlatformCloseAction(landingPlatform));

		//Sync
		childList.PushAction(new SetBuildingFreedomAction(building, true));
		childList.PushAction(new SyncAction());
	}

	public override void Start()
	{
		base.Start();
		if (!carryObject.HasStorageSpace())
		{
			RaiseFailEvent($"Cannot start collection because {shipObject.name} does not have any space for {resourceData.resourceName}");

		}
		if (!producer.CanProducesResource(resourceData))
		{
			RaiseFailEvent($"Cannot start Collection because {producer.GetType().ToString()} does produce {resourceData.resourceName}");
		}
	}
}
