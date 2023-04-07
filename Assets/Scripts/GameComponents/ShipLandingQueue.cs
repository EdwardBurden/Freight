using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//TODO, move into each land
public class ShipLandingQueue : MonoBehaviour
{
	public static event Action<Ship> ShipQueueAvailableEvent;
	private List<Ship> queue;

	[SerializeField] private List<LandingPlatform> landingPlatforms;

	private List<PlatformShips> platformQueue;

	private void Awake()
	{
		queue = new List<Ship>();
		platformQueue = new List<PlatformShips>();
		foreach (LandingPlatform landingPlatform in landingPlatforms)
		{
			//landingPlatform.CloseAnimationCompleteEvent
			platformQueue.Add(new PlatformShips(landingPlatform));
		}

		if (platformQueue.Count == 0)
		{
			Debug.LogError("NO platforms");
			return;
		}
	}

	private LandingPlatform GetMostAvailblePlatform()
	{
		int lowest = int.MaxValue;
		int index = 0;
		for (int i = 0; i < platformQueue.Count; i++)
		{
			if (queue.Count() < lowest)
			{
				lowest = queue.Count();
				index = i;
			}
		}
		return platformQueue[index];
	}

	public void AddToQueue(Ship ship)
	{
		PlatformShips platformShips = GetMostAvailblePlatform();
		platformShips.Add(ship);

	}

	public void RemoveFromQueue(Ship ship)
	{

	}

	internal bool IsFree(LandingPlatform landingPlatform)
	{
		throw new NotImplementedException();
	}

	private struct PlatformShips
	{
		public LandingPlatform platform;
		private HashSet<Ship> queedShips;

		public PlatformShips(LandingPlatform landingPlatform)
		{
			this.platform = landingPlatform;
			this.queedShips = new HashSet<Ship>();
		}

		public int Count()
		{
			return queedShips.Count;
		}

		public void Add(Ship ship)
		{
			queedShips.Add(ship);
		}

		public void Remove(Ship ship)
		{
			queedShips.Remove(ship);
		}
	}

}
