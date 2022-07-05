using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingPlatform : MonoBehaviour
{
	[SerializeField]
	private GameObject freightSpawnTransform;

	internal Vector3 GetLandingPosition()
	{
		return this.transform.position;
	}

	internal Quaternion GetLandingRotation()
	{
		return this.transform.rotation;
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
