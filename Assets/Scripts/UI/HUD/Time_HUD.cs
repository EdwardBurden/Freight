using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;
namespace UI
{
	public class Time_HUD : HUD
	{
		[SerializeField] private TMP_Text secondsText;
		[SerializeField] private TMP_Text minutesText;
		[SerializeField] private Button toggleTimeButton;

		private void OnEnable()
		{
			TimeController.SecondsChangeEvent += OnSecondChange;
			TimeController.MinutesChangeEvent += OnMinuteChange;
		}

		private void OnDisable()
		{
			TimeController.SecondsChangeEvent -= OnSecondChange;
			TimeController.MinutesChangeEvent -= OnMinuteChange;
		}

		private void Start()
		{
			toggleTimeButton.onClick.RemoveAllListeners();
			toggleTimeButton.onClick.AddListener(ToggleTime);
		}

		private void ToggleTime()
		{
			if (TimeController.instance.isPaused)
			{
				TimeController.instance.ResumeTime();
			}
			else
			{
				TimeController.instance.PauseTime();
			}

		}

		private void OnMinuteChange(int minute)
		{
			minutesText.text = minute.ToString();

		}

		private void OnSecondChange(int second)
		{
			secondsText.text = second.ToString();
		}
	}
}