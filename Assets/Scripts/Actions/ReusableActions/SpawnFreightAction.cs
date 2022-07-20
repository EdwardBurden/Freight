using ActionLists;
using UnityEngine;

public class SpawnFreightAction : Action
{
	private ResourceDefintion resourceData;
	private LandingPlatform landingPlatform;

	public SpawnFreightAction(ResourceDefintion resourceData, LandingPlatform landingPlatform) : base(true)
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
		FreightObject freight = GameObject.Instantiate(resourceData.freightPrefab, transform) as FreightObject;
		freight.resourceData = resourceData;
	}
}
