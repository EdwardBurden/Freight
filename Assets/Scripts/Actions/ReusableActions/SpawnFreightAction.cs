using ActionLists;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFreightAction : Action
{
	private ResourceData resourceData;
	private LandingPlatform landingPlatform;

	public SpawnFreightAction(ResourceData resourceData, LandingPlatform landingPlatform)
	{
		this.resourceData = resourceData;
		this.landingPlatform = landingPlatform;
	}

	public override void OnUpdate()
	{
		isFinished = true;
	}

	public override void OnInitialise()
	{
		base.OnInitialise();
		Transform transform = landingPlatform.GetFreightSpawnTransform();
		GameObject.Instantiate(resourceData.freightPrefab, transform);
	}
}
