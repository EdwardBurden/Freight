using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace UI
{
	public class TurnRewardOptionScreen : Screen
	{
		public static string PATH = "UI/Screens/TurnRewardOptionScreen";

		[SerializeField] private Transform options;
		[SerializeField] private TMP_Text turnText;
		[SerializeField] private GameObject turnRewardWidget;

		private Action<TurnReward> turnRewardCallback;

		public static void Create(TurnRewardsOption turnRewardsOption, Action<TurnReward> callBack)
		{
			TurnRewardOptionScreen screen = GameUIController.instance.OpenScreen(PATH) as TurnRewardOptionScreen;
			screen.Init(turnRewardsOption, callBack);
		}

		private void Init(TurnRewardsOption turnRewardsOption, Action<TurnReward> callBack)
		{
			turnRewardCallback = callBack;
			GameObjectUtils.Clear(options);
			turnText.text = turnRewardsOption.rewardTurn.ToString();
			ShowActions(turnRewardsOption.turnRewards);
		}

		private void ShowActions(TurnReward[] turnRewards)
		{
			foreach (TurnReward turnReward in turnRewards)
			{
				GameObject gameObject = Instantiate(turnRewardWidget, options);
				//gameObject.ShowAction(turnReward, () => OptionSelected(action));
			}
		}

		private void OptionSelected(TurnReward turnReward)
		{
			turnRewardCallback?.Invoke(turnReward);
			turnRewardCallback = null;
		}

		private void OnDestroy()
		{
			turnRewardCallback?.Invoke(null);
		}
	}
}
