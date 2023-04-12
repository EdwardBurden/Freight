using ActionLists;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForPlatformToBeFreeAction : Action
{
	private LandingPlatform platform;
	private Ship ship;
	private Vector3 navPos;

	public WaitForPlatformToBeFreeAction(LandingPlatform platform, Ship ship, Vector3 navPos) : base(true)
	{
		this.platform = platform;
		this.ship = ship;
		this.navPos = navPos;
	}

	public override void OnInitialise()
	{
		base.OnInitialise();
		platform.RegisterWaitingShip(ship);
		platform.OnShipLeftPlatformEvent += OnQueueUpdated;
		if (platform.WaitingShipsCount() == 1)
		{
			isFinished = true;
			platform.OnShipLeftPlatformEvent -= OnQueueUpdated;
		}
		else
		{
			owner.InsertActionBefore(this, new CirclePositionAction(navPos, ship, 4));
		}
	}

	public override void OnUpdate()
	{

		//add to parent list this 		childList.PushAction(new MovenNearAction(navtransform.position, shipObject, 5, true));
		//TODO make ship orbit
		//throw new System.NotImplementedException();
	}

	private void OnQueueUpdated(Ship ship)
	{
		if (this.ship == ship)
		{
			isFinished = true;
			platform.OnShipLeftPlatformEvent -= OnQueueUpdated;
			owner.RemoveActionBefore(this);
		}
	}

	public override void Cancel()
	{
		base.Cancel();
		platform.ShipLeft(ship);
		platform.OnShipLeftPlatformEvent -= OnQueueUpdated;
	}
}
