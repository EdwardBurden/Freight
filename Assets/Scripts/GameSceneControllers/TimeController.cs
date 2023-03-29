using ActionLists;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : Singleton<TimeController>, IGameController
{
	public static event Action<int,int> SecondsChangeEvent;
	public static event Action<int> MinutesChangeEvent;

	public static int totalSeconds;
	public static int seconds;
	public static int minute;

	private float secondToRealTime = 1f;
	private float timer;

	private float fixedDeltaTime;
	public bool isPaused { get; private set; }

	private void Awake()
	{
		_instance = this;
	}

	public void Init()
	{
		isPaused = true;
		fixedDeltaTime = Time.fixedDeltaTime;
	}

	private void Update()
	{
		if (isPaused)
		{
			return;
		}
		timer -= Time.deltaTime;

		if (timer <= 0)
		{
			totalSeconds++;
			seconds++;
			if (totalSeconds % 60 == 0)
			{
				minute++;
				MinutesChangeEvent?.Invoke(minute);
				seconds = 0;
			}
			SecondsChangeEvent?.Invoke(totalSeconds, seconds);
			timer = secondToRealTime;
		}
	}

	public void PauseTime()
	{
		isPaused = true;
		Time.timeScale = 0f;
		Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
	}

	public void ResumeTime()
	{
		isPaused = false;
		Time.timeScale = 1f;
		Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
	}

	public void StartTime()
	{
		totalSeconds = 0;
		minute = 0;
		timer = secondToRealTime;
		ResumeTime();
	}
}
