using ActionLists;
using UnityEngine;

public class CollectOrder : ShipOrder
{
	private CarryObject carryObject;
	private IProducer producer;
	private ResourceDefintion resourceData;

	public CollectOrder(ShipActionDefintion shipActionDefintion, Ship shipObject, IProducer target, ResourceDefintion resourceTarget) : base(shipActionDefintion, shipObject)
	{
		this.carryObject = shipObject.GetComponent<CarryObject>();
		this.producer = target;
		this.resourceData = resourceTarget;
	}

	public override void Setup()
	{
		base.Setup();
		LandingPlatform landingPlatform = producer.GetProducerLandingPlatform();
		Transform navtransform = producer.GetProducerNavigationPostion();
		Vector3 position = landingPlatform.GetLandingPosition();
		LevelBuilding building = (LevelBuilding)producer;

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
