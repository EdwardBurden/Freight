using ActionLists;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentFreightToPlatformAction : Action
{
	private LandingPlatform landingPlatform;
	private GameObject ship;
	private ResourceData resourceData;

	public ParentFreightToPlatformAction(LandingPlatform landingPlatform, GameObject ship, ResourceData resourceData)
	{
		this.landingPlatform = landingPlatform;
		this.ship = ship;
		this.resourceData = resourceData;
	}

	public override void OnUpdate()
	{
		isFinished = true;
	}

	public override void OnInitialise()
	{
		base.OnInitialise();
		//GET Selected FreightFromShip, for now just get freight gameobject 

		Transform freight = ship.transform.GetChild(1);
		freight.parent = landingPlatform.GetFreightSpawnTransform();
	}
}
