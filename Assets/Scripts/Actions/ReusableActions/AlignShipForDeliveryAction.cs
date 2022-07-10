using ActionLists;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignShipForDeliveryAction : HierarchicalAction
{
	CarryObject carryObject;
	LandingPlatform landingPlatform;
	Vector3 offset;
	ResourceData resourceData;
	public AlignShipForDeliveryAction(CarryObject carryObject, ResourceData resourceData, LandingPlatform landingPlatform) : base()
	{
		this.carryObject = carryObject;
		this.landingPlatform = landingPlatform;
		this.resourceData = resourceData;
	}

	public override void OnInitialise()
	{
		base.OnInitialise();
		offset = carryObject.GetFirstStorageTransformOfResource(resourceData).localPosition;
		Vector3 position = landingPlatform.GetLandingPosition();
		Quaternion rotation = landingPlatform.GetLandingRotation();
		actionList.PushAction(new RotateToAction(rotation, 40, carryObject.gameObject, true)); //block
		actionList.PushAction(new MoveToAction(position - (rotation * offset), 2, carryObject.gameObject, false)); //no block
	}
}
