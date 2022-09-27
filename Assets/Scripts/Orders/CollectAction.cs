using ActionLists;
using System.Collections;
using System.Collections.Generic;
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

	public override void Start()
	{
		base.Start();
		if (!CheckShipIsValid() || !CheckProducerIsValid())
		{
			RaiseFailEvent("Custom Error here");
		}
	}

	private bool CheckShipIsValid()
	{
		if (!carryObject.HasStorageSpace())
		{
			//Notifications.instance.AddNotification(shipObject.shipName, $"Cannot start collection because {shipObject.name} does not have any space for {resourceData.resourceName}");
			return false;
		}
		return true;
	}

	private bool CheckProducerIsValid()
	{
		if (!producer.ProducesResource(resourceData))
		{
			//Notifications.instance.AddNotification(shipObject.shipName, $"Cannot start Collection because {producer.GetType().ToString()} does produce {resourceData.resourceName}");
			return false;
		}
		return true;
	}

	public override void Setup()
	{
		base.Setup();
		LandingPlatform landingPlatform = producer.GetProducerLandingPlatform();
		Vector3 position = landingPlatform.GetLandingPosition();

		//Move
		childList.PushAction(new LookAtAction(position, shipObject, true));
		childList.PushAction(new AlignShipForCollectionAction(shipObject, carryObject, landingPlatform));

		//Collect
		childList.PushAction(new SpawnFreightAction(resourceData, landingPlatform));
		childList.PushAction(new AnimateLoadingPlatformOpenAction(landingPlatform));

		childList.PushAction(new ParentFreightToShipAction(landingPlatform, carryObject));
		childList.PushAction(new AnimateLoadingPlatformCloseAction(landingPlatform));
		childList.PushAction(new SyncAction());
	}

	public override void Finish()
	{
		//TODO make this payment a new action instead
		int price = producer.GetSellPrice(resourceData);
		Wallet wallet = GameObject.FindObjectOfType<Wallet>();
		wallet.CreateAndAddBuyTransaction(price, producer as Building, shipObject, resourceData);
	}
}
