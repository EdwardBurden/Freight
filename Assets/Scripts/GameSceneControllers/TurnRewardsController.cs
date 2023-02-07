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

public class TurnRewardsController : MonoBehaviour, IGameController
{
	public static event Action<int> TurnStartedEvent;
	public static event Action<int> TurnEndedEvent;
	public static event Action<int> LastTurnEndedEvent;

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
			LastTurnEndedEvent?.Invoke(turn); //maybe move logic ot gamemanger instead
			return;
		}
		TurnEndedEvent?.Invoke(turn);
		turn++;
		StartTurn();
	}

	public void StartTurn()
	{
		IslandBlock islandBlock = TerrainController.instance.AddNextBlock();
		PathfindingController.instance.UpdatedGrid(islandBlock.transform.position, 10);
		TurnRewardsOption turnRewardsOption = FindTurnRewards(turn);
		if (turnRewardsOption!=null)
		{
			//if the rewarss exist for this turn 
			//Close active UI
			//Show decisions UI (if any)
			//add hook to listen to response
			// move OnTurnStarted?.Invoke(turn); to reposnse 
			TurnRewardOptionScreen.Create(turnRewardsOption, OnRewardSelected);
		}
		else
		{
			//else say 	OnTurnStarted?.Invoke(turn);
			//Place new block
			//move cmaera to show block
			TurnStartedEvent?.Invoke(turn);
		}
	}


	private TurnRewardsOption FindTurnRewards(int turn)
	{
		for (int i = 0; i < turnRewardsOptions.Length; i++)
		{
			if (turnRewardsOptions[i].rewardTurn == turn)
			{
				return turnRewardsOptions[i];
			}
		}
		return null;
	}


	private void OnRewardSelected(TurnReward turnReward)
	{
		if (turnReward == null)
		{
			TurnStartedEvent?.Invoke(turn);
			return;
		}
		//	GameUIController.instance.CloseAllScreens();
		turnReward.OnRewardSelected(OnRewardFinsihed);

		//Do more here depending on turnreward , show a preview for player to select things
		//OnTurnStarted?.Invoke(turn);
	}

	private void OnRewardFinsihed(bool success)
	{
		TurnStartedEvent?.Invoke(turn);
	}
}
