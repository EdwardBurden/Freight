using ActionLists;

public class ParentFreightToPlatformAction : Action
{
	private LandingPlatform landingPlatform;
	private CarryObject ship;
	private ResourceDefintion resourceData;

	public ParentFreightToPlatformAction(LandingPlatform landingPlatform, CarryObject ship, ResourceDefintion resourceData)
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
		FreightObject freight = ship.GetFreightOfResource(resourceData);
		freight.transform.parent = landingPlatform.GetFreightSpawnTransform();
	}
}
