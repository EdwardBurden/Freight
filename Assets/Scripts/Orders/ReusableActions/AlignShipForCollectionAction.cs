using ActionLists;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AlignShipForCollectionAction : HierarchicalAction
{
	private Ship shipObject;
	private CarryObject carryObject;
	private LandingPlatform landingPlatform;
	private Vector3 offset;

	public AlignShipForCollectionAction(Ship shipObject, CarryObject carryObject, LandingPlatform landingPlatform) : base()
	{
		this.shipObject = shipObject;
		this.carryObject = carryObject;
		this.landingPlatform = landingPlatform;
	}

	public override void OnInitialise()
	{
		base.OnInitialise();
		offset = carryObject.GetFirstEmptyStorageTransform().localPosition;
		Vector3 position = landingPlatform.GetLandingPosition();
		Quaternion landingDirection = landingPlatform.GetLandingDirection(shipObject);

		childList.PushAction(new MoveToAction(position - (landingDirection * offset), shipObject, true));
		childList.PushAction(new RotateToAction(landingDirection, shipObject, true));
	}

}