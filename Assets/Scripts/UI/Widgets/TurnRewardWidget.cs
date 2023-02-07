using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{

	public class TurnRewardWidget : MonoBehaviour
	{
		[SerializeField] private TMP_Text rewardNameText;
		[SerializeField] private TMP_Text rewardDescriptionText;
		[SerializeField] private Image rewardIconImage;
		[SerializeField] private Button rewardButton;

		private TurnReward turnReward;

		public virtual void ShowReward(TurnReward turnReward, Action<TurnReward> callback)
		{
			this.turnReward = turnReward;
			rewardNameText.text = turnReward.rewardName;
			rewardDescriptionText.text = turnReward.rewardDescription;
			rewardIconImage.sprite = turnReward.rewardIcon;
			rewardButton.onClick.AddListener(() => callback(this.turnReward));
		}
	}
}
