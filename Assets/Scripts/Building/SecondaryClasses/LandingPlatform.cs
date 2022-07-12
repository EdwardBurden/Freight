using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingPlatform : MonoBehaviour
{
	[SerializeField]
	private GameObject freightSpawnTransform;

	[SerializeField]
	private GameObject landingSpot;

	internal Vector3 GetLandingPosition()
	{
		return landingSpot.transform.position;
	}

	internal Quaternion GetLandingRotation()
	{
		return landingSpot.transform.rotation;
	}

	internal Transform GetFreightSpawnTransform()
	{
		return freightSpawnTransform.transform;
	}

	internal GameObject GetActiveFreight()
	{
		return freightSpawnTransform.transform.GetChild(0).gameObject;
	}
}
