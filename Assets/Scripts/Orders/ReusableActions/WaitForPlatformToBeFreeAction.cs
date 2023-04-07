using ActionLists;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForPlatformToBeFreeAction : Action
{
	private LandingPlatform platform;
	private Ship ship;

	public WaitForPlatformToBeFreeAction(LandingPlatform platform, Ship ship) : base(true)
	{
		this.platform = platform;
		this.ship = ship;
	}

	public override void OnInitialise()
	{
		base.OnInitialise();
		platform.RegisterWaitingShip(ship);
		platform.OnQueueUpdatedEvent += OnQueueUpdated;
	}

	public override void OnUpdate()
	{
		//throw new System.NotImplementedException();
	}

	private void OnQueueUpdated(Ship ship)
	{
		if (this.ship == ship)
		{
			isFinished = true;
			platform.RemoveWaitingShip(ship);
			platform.OnQueueUpdatedEvent -= OnQueueUpdated;
		}
	}

	public override void Cancel()
	{
		base.Cancel();
		platform.RemoveWaitingShip(ship);
		platform.OnQueueUpdatedEvent -= OnQueueUpdated;
	}
}
