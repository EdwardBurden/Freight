using ActionLists;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignShipForCollectionAction : HierarchicalAction
{
	CarryObject carryObject;
	LandingPlatform landingPlatform;
	Vector3 offset;
	public AlignShipForCollectionAction(CarryObject carryObject, LandingPlatform landingPlatform) : base()
	{
		this.carryObject = carryObject;
		this.landingPlatform = landingPlatform;
	}

	public override void OnInitialise()
	{
		base.OnInitialise();
		offset = carryObject.GetFirstEmptyStorageTransform().localPosition;
		Vector3 position = landingPlatform.GetLandingPosition();
		Quaternion rotation = landingPlatform.GetLandingRotation();
		actionList.PushAction(new RotateToAction(rotation, 40, carryObject.gameObject, false)); //block
		actionList.PushAction(new MoveToAction(position - (rotation * offset), 2, carryObject.gameObject, false)); //no block
	}
}