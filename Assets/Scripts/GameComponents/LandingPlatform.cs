using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class LandingPlatform : MonoBehaviour
{
	public event Action<Ship> OnQueueUpdatedEvent;
	public event Action OpenAnimationCompleteEvent;
	public event Action CloseAnimationCompleteEvent;

	[SerializeField]
	private GameObject freightSpawnTransform;

	[SerializeField]
	private GameObject landingSpot;

	private HashSet<Ship> waitingShips = new HashSet<Ship>();



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
		if (waitingShips.Count == 1)
		{
			RemoveWaitingShip(ship);
		}
	}

	internal void RemoveWaitingShip(Ship ship)
	{
		waitingShips.Remove(ship);
		OnQueueUpdatedEvent?.Invoke(ship);
	}

	internal int WaitingShipsCount()
	{
		return waitingShips.Count;
	}
}
