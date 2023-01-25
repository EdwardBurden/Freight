using System;
using System.Collections;
using System.Collections.Generic;
using Terrain;
using UnityEngine;

public class TurnController : Singleton<TurnController>
{
	public static event Action OutOfTurnsEvent;

	private void Awake()
	{
		_instance = this;
	}

	private void OnEnable()
	{
		SeasonsController.YearEndedEvent += SeasonsController_YearEndedEvent; ;
	}

	private void SeasonsController_YearEndedEvent(int obj)
	{
		if (HasReachedEnd())
		{
			OutOfTurnsEvent?.Invoke();
		}

		//disbale input
		//pause game time
		//Close active UI
		//Show decisions UI (if any)
		//Place new block
		//move cmaera to show block
		//Place new block
		IslandBlock islandBlock = TerrainController.instance.AddNextBlock();
		PathfindingController.instance.UpdatedGrid(islandBlock.transform.position, 10); //2 is temp
		//resume time
		//enable input
	}

	private bool HasReachedEnd()
	{
		return TerrainController.instance.CanPlaceBlock();

	}
}
