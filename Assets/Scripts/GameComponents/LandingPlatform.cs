using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

public class LandingPlatform : MonoBehaviour
{
	public event Action<Ship> OnShipLeftPlatformEvent;
	public event Action OpenAnimationCompleteEvent;
	public event Action CloseAnimationCompleteEvent;

	[SerializeField]
	private GameObject freightSpawnTransform;

	[SerializeField]
	private GameObject landingSpot;

	private List<Ship> waitingShips = new List<Ship>();



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

	public Vector3 GetLandingDirection()
	{
		return landingSpot.transform.forward;
	}

	internal Quaternion GetLandingDirection(Ship shipObject)
	{
		Vector3 direction = GetLandingDirection();
		if (Vector3.Dot(shipObject.transform.forward, direction) < 0)
		{
			direction = -direction;
		}
		return Quaternion.LookRotation(direction);
	}

	public void RegisterWaitingShip(Ship ship)
	{
		waitingShips.Add(ship);

	}

	internal int WaitingShipsCount()
	{
		return waitingShips.Count;
	}

	public void ShipLeft(Ship ship)
	{
		waitingShips.Remove(ship);
		OnShipLeftPlatformEvent?.Invoke(waitingShips[waitingShips.Count - 1]);
	}
}
