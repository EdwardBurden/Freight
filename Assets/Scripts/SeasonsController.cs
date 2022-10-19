using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public enum SeasonType
{
	Spring,
	Summer,
	Autumn,
	Winter
}

public class SeasonsController : Singleton<SeasonsController>
{
	public static event Action<SeasonType> SeasonStartedEvent;
	public static event Action<SeasonType> SeasonEndedEvent;

	[SerializeField] private Transform light; //for testing atm
	[SerializeField] private int seasonTimeInSeconds = 5;

	private SeasonType? season = null;

	private void Awake()
	{
		_instance = this;
	}

	private void OnEnable()
	{
		TimeController.SecondsChangeEvent += TimeController_SecondsChangeEvent;
	}

	private void OnDisable()
	{
		TimeController.SecondsChangeEvent -= TimeController_SecondsChangeEvent;
	}

	private void TimeController_SecondsChangeEvent(int seconds)
	{
		if ((seconds % seasonTimeInSeconds) == 0)
		{
			StartNextSeason();
		}
	}

	public void Init()
	{
		season = null;
	}

	public void StartNextSeason()
	{
		if (season != null)
		{
			SeasonEndedEvent?.Invoke(season.Value);
		}
		UpdateSeason();
		SeasonStartedEvent?.Invoke(season.Value);
	}

	private void UpdateSeason()
	{
		if (season == null)
		{
			season = SeasonType.Spring;
			return;
		}

		Array seasons = Enum.GetValues(typeof(SeasonType));
		if ((int)season.Value >= seasons.Length - 1)
		{
			season = SeasonType.Spring;
		}
		else
		{
			season++;
		}

		float rotation = (float)season - (seasons.Length / 2.0f);
		light.transform.localEulerAngles = (new Vector3(rotation * 20, rotation * 45, 0));
	}
}
