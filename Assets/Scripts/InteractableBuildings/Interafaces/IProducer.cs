using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProducer
{
	public ResourceData[] GetProducedResources();
	public Vector3 GetLandingPosition();
	public Quaternion GetLandingRotation();
	GameObject GetLandingPlatform();
}
