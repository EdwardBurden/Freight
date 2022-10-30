using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeHUD : MonoBehaviour
{
	[SerializeField] private TMP_Text seconds;
	[SerializeField] private TMP_Text minutes;

	[SerializeField] private Button toggleTime;

	private void OnEnable()
	{
		TimeController.SecondsChangeEvent += TimeController_SecondsChangeEvent;
		TimeController.MinutesChangeEvent += TimeController_MinutesChangeEvent;
	}

	private void Start()
	{
		toggleTime.onClick.RemoveAllListeners();
		toggleTime.onClick.AddListener(ToggleTime);
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

	private void TimeController_MinutesChangeEvent(int obj)
	{
		minutes.text = obj.ToString();

	}

	private void TimeController_SecondsChangeEvent(int obj)
	{
		seconds.text = obj.ToString();
	}

	private void OnDisable()
	{
		TimeController.SecondsChangeEvent -= TimeController_SecondsChangeEvent;
		TimeController.MinutesChangeEvent -= TimeController_MinutesChangeEvent;
	}
}
