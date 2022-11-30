using ActionLists;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignShipForDeliveryAction : HierarchicalAction //make into single actions if possible
{
	private Ship shipObject;
	private CarryObject carryObject;
	private LandingPlatform landingPlatform;
	private Vector3 offset;
	private ResourceDefintion resourceData;

	public AlignShipForDeliveryAction(Ship shipObject, CarryObject carryObject, ResourceDefintion resourceData, LandingPlatform landingPlatform) : base()
	{
		this.shipObject = shipObject;
		this.carryObject = carryObject;
		this.landingPlatform = landingPlatform;
		this.resourceData = resourceData;
	}

	public override void OnInitialise()
	{
		base.OnInitialise();
		offset = carryObject.GetFirstStorageTransformOfResource(resourceData).localPosition;
		Vector3 position = landingPlatform.GetLandingPosition();
		Quaternion landingDirection = landingPlatform.GetLandingDirection(shipObject);

		childList.PushAction(new MoveToAction(position - (landingDirection * offset), shipObject, true));
		childList.PushAction(new RotateToAction(landingDirection, shipObject, true));
	}
}
