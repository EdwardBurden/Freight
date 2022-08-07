using ActionLists;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectAction : ShipAction
{
	private CarryObject carryObject;
	private IProducer producer;
	private ResourceDefintion resourceData;


	public CollectAction(ShipActionDefintion shipActionDefintion, Ship shipObject, IProducer target, ResourceDefintion resourceTarget) : base(shipActionDefintion, shipObject)
	{
		this.carryObject = shipObject.GetComponent<CarryObject>();
		this.producer = target;
		this.resourceData = resourceTarget;
		Reset();
	}

	public override void OnInitialise()
	{
		base.OnInitialise();
		if (!CheckShipIsValid() || !CheckProducerIsValid())
		{
			failed = true;
			actionList.Clear();
			isFinished = true;
		}
	}

	private bool CheckShipIsValid()
	{
		if (!carryObject.HasStorageSpace())
		{
			NotificationManager.instance.AddNotification(shipObject.shipName, $"Cannot start collection because {shipObject.name} does not have any space for {resourceData.resourceName}");
			return false;
		}
		return true;
	}

	private bool CheckProducerIsValid()
	{
		if (!producer.ProducesResource(resourceData))
		{
			NotificationManager.instance.AddNotification(shipObject.shipName, $"Cannot start Collection because {producer.GetType().ToString()} does produce {resourceData.resourceName}");
			return false;
		}
		return true;
	}

	public override void Reset()
	{
		base.Reset();
		LandingPlatform landingPlatform = producer.GetProducerLandingPlatform();
		Vector3 position = landingPlatform.GetLandingPosition();

		//Move
		actionList.PushAction(new LookAtAction(position, shipObject, true));
		actionList.PushAction(new AlignShipForCollectionAction(shipObject, carryObject, landingPlatform));

		//Collect
		actionList.PushAction(new SpawnFreightAction(resourceData, landingPlatform));
		actionList.PushAction(new AnimateLoadingPlatformOpenAction(landingPlatform));

		actionList.PushAction(new ParentFreightToShipAction(landingPlatform, carryObject));
		actionList.PushAction(new AnimateLoadingPlatformCloseAction(landingPlatform));
		actionList.PushAction(new SyncAction());
	}

	public override void OnFinish()
	{
		WalletManager.instance.CreateAndAddBuyTransaction(resourceData.price, producer as Building, shipObject, resourceData);
	}
}
