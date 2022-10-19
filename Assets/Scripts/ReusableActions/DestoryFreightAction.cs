using ActionLists;
using UnityEngine;

public class DestoryFreightAction : Action
{
	private LandingPlatform landingPlatform;

	public DestoryFreightAction( LandingPlatform landingPlatform)
	{
		this.landingPlatform = landingPlatform;
	}

	public override void OnUpdate()
	{
		isFinished = true;
	}

	public override void OnInitialise()
	{
		base.OnInitialise();
		GameObject.Destroy(landingPlatform.GetActiveFreight());
	}
}
