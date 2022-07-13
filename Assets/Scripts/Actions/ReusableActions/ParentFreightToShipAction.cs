using ActionLists;
using UnityEngine;

public class ParentFreightToShipAction : Action
{
	private LandingPlatform landingPlatform;
	private CarryObject ship;

	public ParentFreightToShipAction(LandingPlatform landingPlatform, CarryObject ship)
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
		freight.transform.parent = ship.GetFirstEmptyStorageTransform();
	}
}
