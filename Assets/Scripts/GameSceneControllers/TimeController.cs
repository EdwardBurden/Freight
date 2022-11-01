using ActionLists;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : Singleton<TimeController>
{
	public static event Action<int> SecondsChangeEvent;
	public static event Action<int> MinutesChangeEvent;

	public static int second;
	public static int minute;

	private float secondToRealTime = 1f;
	private float timer;

	private float fixedDeltaTime;
	internal bool isPaused;

	private void Awake()
	{
		_instance = this;
		this.fixedDeltaTime = Time.fixedDeltaTime;
	}

	private void Start()
	{
		second = 0;
		minute = 0;
		timer = secondToRealTime;

	}

	private void Update()
	{
		timer -= Time.deltaTime;

		if (timer <= 0)
		{
			second++;
			if (second >= 60)
			{
				minute++;
				MinutesChangeEvent?.Invoke(minute);
				second = 0;
			}
			SecondsChangeEvent?.Invoke(second);
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
}
