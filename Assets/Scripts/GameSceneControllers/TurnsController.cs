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

public class TurnsController : MonoBehaviour, IGameController
{
	public static event Action<int> TurnStartedEvent;
	public static event Action<int> TurnEndedEvent;
	public static event Action<int> LastTurnEndedEvent;

	[SerializeField] private int turnTimeInSeconds = 5;
	[SerializeField] private TurnRewardsOption[] turnRewardsOptions;

	private int turn;
	private bool active;

	private void OnEnable()
	{
		TimeController.SecondsChangeEvent += OnSecondsChange;
		GameManager.GameStartEvent += OnGameStarted;
	}

	private void OnDisable()
	{
		TimeController.SecondsChangeEvent -= OnSecondsChange;
	}

	public void Init()
	{
		turn = 0;
		active = false;
	}

	private void OnSecondsChange(int seconds)
	{
		if (active)
		{
			if ((seconds % turnTimeInSeconds) == 0)
			{
				StartNextTurn();
			}
		}
	}

	private void OnGameStarted()
	{
		active = true;
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

	private void StartTurn()
	{
		TurnRewardsOption turnRewardsOption = FindTurnRewards(turn);
		if (turnRewardsOption != null)
		{
			TurnRewardOptionScreen.Create(turnRewardsOption, OnRewardSelected);
		}
		else
		{
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
