using ActionLists;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignShipForDeliveryAction : HierarchicalAction
{
	private ShipObject shipObject;
	private CarryObject carryObject;
	private LandingPlatform landingPlatform;
	private Vector3 offset;
	private ResourceData resourceData;

	public AlignShipForDeliveryAction(ShipObject shipObject, CarryObject carryObject, ResourceData resourceData, LandingPlatform landingPlatform) : base()
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
		Quaternion rotation = landingPlatform.GetLandingRotation();

		actionList.PushAction(new RotateToAction(rotation, shipObject, false)); //block
		actionList.PushAction(new MoveToAction(position - (rotation * offset), shipObject, false)); //no block
	}
}
