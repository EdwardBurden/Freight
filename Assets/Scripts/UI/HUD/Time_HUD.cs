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
		[SerializeField] private Button playTimeButton;
		[SerializeField] private Button pauseTimeButton;

		private void OnEnable()
		{
			TimeController.SecondsChangeEvent += OnSecondChange;
			TimeController.MinutesChangeEvent += OnMinuteChange;

			GameManager.GameStartEvent += OnGameStart;

			playTimeButton.onClick.AddListener(ToggleTime);
			pauseTimeButton.onClick.AddListener(ToggleTime);
		}

		private void OnDisable()
		{
			TimeController.SecondsChangeEvent -= OnSecondChange;
			TimeController.MinutesChangeEvent -= OnMinuteChange;

			GameManager.GameStartEvent -= OnGameStart;

			playTimeButton.onClick.RemoveAllListeners();
			pauseTimeButton.onClick.RemoveAllListeners();
		}

		private void OnGameStart()
		{
			ToggleButtons();
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
			ToggleButtons();
		}

		private void OnMinuteChange(int minute)
		{
			minutesText.text = minute.ToString();

		}

		private void OnSecondChange(int toalseconds, int seconds)
		{
			secondsText.text = seconds.ToString();
		}

		private void ToggleButtons() {
			pauseTimeButton.gameObject.SetActive(TimeController.instance.isPaused);
			playTimeButton.gameObject.SetActive(!TimeController.instance.isPaused);

		}
	}
}