using ActionLists;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreePlatformAction : Action
{
	private LandingPlatform platform;
	private Ship ship;

	public FreePlatformAction(LandingPlatform platform, Ship ship) : base(true)
	{
		this.platform = platform;
		this.ship = ship;
	}

	public override void OnUpdate()
	{
		platform.ShipLeft(ship);
		isFinished = true;
	}
}
