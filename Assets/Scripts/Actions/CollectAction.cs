using ActionLists;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectAction : ShipAction
{
	private CarryObject carryObject;
	private IProducer producer;
	private ResourceData resourceData;


	public CollectAction(ShipActionDefintion shipActionDefintion, ShipObject shipObject, IProducer target, ResourceData resourceTarget) : base(shipActionDefintion, shipObject)
	{
		this.carryObject = shipObject.GetComponent<CarryObject>();
		this.producer = target;
		this.resourceData = resourceTarget;
		CreateActions();
	}

	private void CreateActions()
	{
		LandingPlatform landingPlatform = producer.GetLandingPlatform();
		Vector3 position = landingPlatform.GetLandingPosition();

		//Move
		actionList.PushAction(new LookAtAction(position, shipObject, true));
		actionList.PushAction(new SyncAction());
		actionList.PushAction(new AlignShipForCollectionAction(shipObject, carryObject, landingPlatform)); //block

		//Collect
		actionList.PushAction(new SpawnFreightAction(resourceData, landingPlatform)); //block
		actionList.PushAction(new AnimateLoadingPlatformOpenAction(landingPlatform));    //not block
		actionList.PushAction(new SyncAction());
		actionList.PushAction(new DelayAction(2));

		actionList.PushAction(new ParentFreightToShipAction(landingPlatform, carryObject)); //block
		actionList.PushAction(new AnimateLoadingPlatformCloseAction(landingPlatform)); //not block
		actionList.PushAction(new SyncAction());
		actionList.PushAction(new DelayAction(2));
	}

	public override void OnInitialise()
	{
		base.OnInitialise();
		if (!CheckShipIsValid() || !CheckProducerIsValid())
		{
			actionList.Clear();
			isFinished = true;
		}

	}

	private bool CheckShipIsValid()
	{
		if (!carryObject.HasStorageSpace())
		{
			NotificationManager.instance.AddNotification(shipObject, $"Cannot start collection because {shipObject.name} does not have any space for {resourceData.resourceName}");
			return false;
		}
		return true;
	}

	private bool CheckProducerIsValid()
	{
		if (!producer.ProducesResource(resourceData))
		{
			NotificationManager.instance.AddNotification(shipObject, $"Cannot start Collection because {producer.GetType().ToString()} does produce {resourceData.resourceName}");
			return false;
		}
		return true;
	}
}
