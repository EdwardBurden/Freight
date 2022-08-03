using ActionLists;
using UnityEngine;

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
		Quaternion rotation = landingPlatform.GetLandingRotation();

		actionList.PushAction(new MoveToAction(position - (rotation * offset), shipObject, true));
		actionList.PushAction(new RotateToAction(rotation, shipObject, true)); 
	}
}