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
		FreightObject freight = landingPlatform.GetActiveFreight().GetComponent<FreightObject>();
		ship.AddFreight(freight);
		isFinished = true;
	}
}
