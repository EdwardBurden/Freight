using System;
using UnityEngine;

public class LandingPlatform : MonoBehaviour
{
	public event Action OpenAnimationCompleteEvent;
	public event Action CloseAnimationCompleteEvent;

	[SerializeField]
	private GameObject freightSpawnTransform;

	[SerializeField]
	private GameObject landingSpot;

	public Vector3 GetLandingPosition()
	{
		return landingSpot.transform.position;
	}

	public Quaternion GetLandingRotation()
	{
		return landingSpot.transform.rotation;
	}

	public Transform GetFreightSpawnTransform()
	{
		return freightSpawnTransform.transform;
	}

	public GameObject GetActiveFreight()
	{
		return freightSpawnTransform.transform.GetChild(0).gameObject;
	}

	public void OpenAniamtionComplete()
	{
		OpenAnimationCompleteEvent?.Invoke();
	}

	public void CloseAniamtionComplete()
	{
		CloseAnimationCompleteEvent?.Invoke();
	}
}
