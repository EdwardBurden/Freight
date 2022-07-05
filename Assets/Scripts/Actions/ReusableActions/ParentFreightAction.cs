using ActionLists;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentFreightAction : Action
{
	private LandingPlatform landingPlatform;
	private GameObject ship;

	public ParentFreightAction(LandingPlatform landingPlatform, GameObject ship)
	{
		this.landingPlatform = landingPlatform;
		this.ship = ship;
	}

	public override void OnUpdate()
	{
		isFinished = true;
	}

	public override void OnInitialise()
	{
		base.OnInitialise();
		GameObject freight = landingPlatform.GetActiveFreight();
		freight.transform.parent = ship.transform;
	}
}
