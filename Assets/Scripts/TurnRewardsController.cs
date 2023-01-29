using System;
using System.Collections;
using System.Collections.Generic;
using Terrain;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class TurnRewardsOption
{
	[SerializeField] public int rewardTurn;
	[SerializeField] public TurnReward[] turnRewards;
}

public class TurnRewardsController : MonoBehaviour
{
	public static event Action<int> OnTurnStarted;
	public static event Action<int> OnTurnEnded;
	public static event Action<int> OnLastTurnEnded;

	[SerializeField] private int turnTimeInSeconds = 5;
	[SerializeField] private TurnRewardsOption[] turnRewardsOptions;

	private int turn;

	private void OnEnable()
	{
		TimeController.SecondsChangeEvent += TimeController_SecondsChangeEvent;
	}

	private void OnDisable()
	{
		TimeController.SecondsChangeEvent -= TimeController_SecondsChangeEvent;
	}

	public void Init()
	{
		turn = 0;
	}

	private void TimeController_SecondsChangeEvent(int seconds)
	{
		if ((seconds % turnTimeInSeconds) == 0)
		{
			StartNextTurn();
		}
	}

	private void StartNextTurn()
	{
		if (!TerrainController.instance.CanPlaceBlock())
		{
			OnLastTurnEnded?.Invoke(turn);
			return;
		}
		OnTurnEnded?.Invoke(turn);
		turn++;
		StartTurn();
	}

	public void StartTurn()
	{
		IslandBlock islandBlock = TerrainController.instance.AddNextBlock();
		PathfindingController.instance.UpdatedGrid(islandBlock.transform.position, 10);
		if (true)
		{
			//if the rewarss exist for this turn 
			//Close active UI
			//Show decisions UI (if any)
			//add hook to listen to response
			// move OnTurnStarted?.Invoke(turn); to reposnse 
			TurnRewardOptionScreen.Create(turnRewardsOptions[0], OnRewardSelected);
		}
		else
		{
			//else say 	OnTurnStarted?.Invoke(turn);
			//Place new block
			//move cmaera to show block
			OnTurnStarted?.Invoke(turn);
		}
	}


	private void OnRewardSelected(TurnReward turnReward)
	{
		//Do more here depending on turnreward , show a preview for player to select things
		OnTurnStarted?.Invoke(turn);
	}
}
