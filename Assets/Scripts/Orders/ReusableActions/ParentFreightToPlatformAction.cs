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
		ship.ReleaseFreight(resourceData, landingPlatform.GetFreightSpawnTransform());
		isFinished = true;
	}
}
