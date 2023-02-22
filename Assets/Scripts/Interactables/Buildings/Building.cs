using OpenCover.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrain;
using UnityEngine;

public class Building : MonoBehaviour
{
	public BuildingDefintion BuildingDefintion { get; set; }
	public LevelBuilding levelBuilding;
	public IslandBlock islandBlock;

	private void Start()
	{
		levelBuilding = Instantiate(BuildingDefintion.buildingLevels[0].buildingPrefab, this.transform);
		levelBuilding.Init(BuildingDefintion.buildingLevels[0], this);
	}

	private void OnEnable()
	{
		TurnsController.TurnStartedEvent += TurnEndedEvent;
		TurnsController.TurnEndedEvent += TurnStartedEvent;
	}

	private void OnDisable()
	{
		TurnsController.TurnStartedEvent -= TurnEndedEvent;
		TurnsController.TurnEndedEvent -= TurnStartedEvent;
	}

	private void TurnStartedEvent(int turn)
	{

	}

	private void TurnEndedEvent(int turn)
	{
		bool needsFulfilled = levelBuilding.AreNeedsFulfilled();
		if (!needsFulfilled)
		{
			LevelDownBuilding();
			return;
		}

		bool wantsFulfilled = levelBuilding.AreWantsFulfilled();
		if (wantsFulfilled)
		{
			LevelUpBuilding();
			return;
		}
	}

	private void LevelDownBuilding()
	{
		//TODO

	}

	private void LevelUpBuilding()
	{
		//TODO

	}
}

