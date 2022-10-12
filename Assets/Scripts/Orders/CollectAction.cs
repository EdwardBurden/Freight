using ActionLists;
using UnityEngine;

public class CollectAction : ShipOrder
{
	private CarryObject carryObject;
	private IProducer producer;
	private ResourceDefintion resourceData;

	public CollectAction(ShipActionDefintion shipActionDefintion, Ship shipObject, IProducer target, ResourceDefintion resourceTarget) : base(shipActionDefintion, shipObject)
	{
		this.carryObject = shipObject.GetComponent<CarryObject>();
		this.producer = target;
		this.resourceData = resourceTarget;
	}

	public override void Setup()
	{
		base.Setup();
		LandingPlatform landingPlatform = producer.GetProducerLandingPlatform();
		Vector3 position = landingPlatform.GetLandingPosition();
		Building building = (Building)producer; ;

		//Move
		childList.PushAction(new LookAtAction(position, shipObject, true));
		childList.PushAction(new AlignShipForCollectionAction(shipObject, carryObject, landingPlatform));
		childList.PushAction(new WaitForBuildingTobeFreeAction(building));

		//Collect
		childList.PushAction(new SetBuildingFreedomAction(building, false));
		childList.PushAction(new SpawnFreightAction(resourceData, landingPlatform));
		childList.PushAction(new AnimateLoadingPlatformOpenAction(landingPlatform));
		childList.PushAction(new ParentFreightToShipAction(landingPlatform, carryObject));
		childList.PushAction(new AnimateLoadingPlatformCloseAction(landingPlatform));
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
		if (!producer.ProducesResource(resourceData))
		{
			RaiseFailEvent($"Cannot start Collection because {producer.GetType().ToString()} does produce {resourceData.resourceName}");
		}
	}

	public override void Finish()
	{
		WalletController wallet = GameObject.FindObjectOfType<WalletController>();
		wallet.CreateAndAddBuyTransaction(resourceData.buyPrice, producer as Building, shipObject, resourceData);
	}
}
