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
		actionList.PushAction(new MoveToAction(position, shipObject, true));

		//Collect
		actionList.PushAction(new AlignShipForCollectionAction(shipObject, carryObject, landingPlatform)); //block
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
			//TODO Alert User
			actionList.Clear();
			isFinished = true;
		}

	}

	private bool CheckShipIsValid()
	{
		//TODO
		//check ship has space to collect
		return true;

	}

	private bool CheckProducerIsValid()
	{
		//TODO
	 //check producer has the resource type
		return true;

	}
}
