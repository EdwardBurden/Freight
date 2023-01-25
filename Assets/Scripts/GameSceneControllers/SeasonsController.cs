using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
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
	public static event Action<int> YearEndedEvent;
	public static event Action<int> YearStartedEvent;

	[SerializeField] private Light light; //for testing atm
	private Vector3 eulerstart;
	private Vector3 eulerTarget;
	private float lightUpdate = 0;
	private float lightStartIntensity = 0;
	private float lightTargetIntensity = 0;

	[SerializeField] private int seasonTimeInSeconds = 5;

	public SeasonType? season = null;
	public int year = 0;

	private void Awake()
	{
		_instance = this;
		eulerstart = light.transform.localEulerAngles;
		eulerTarget = eulerstart;
		lightStartIntensity = light.intensity;
		lightTargetIntensity = lightStartIntensity;
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
		year = 1;
		StartNextSeason();
	}

	public void StartNextSeason()
	{
		if (season != null)
		{
			SeasonEndedEvent?.Invoke(season.Value);
			if (season.Value == SeasonType.Winter)
			{
				YearEndedEvent?.Invoke(year);
			}
		}
		UpdateSeason();
		SeasonStartedEvent?.Invoke(season.Value);
		if (season.Value == SeasonType.Spring)
		{
			year++;
			YearStartedEvent?.Invoke(year);
		}
	}

	private void UpdateSeason()
	{
		Array seasons = Enum.GetValues(typeof(SeasonType));
		if (season == null)
		{
			season = SeasonType.Spring;
		}
		else
		{
			if ((int)season.Value >= seasons.Length - 1)
			{
				season = SeasonType.Spring;
			}
			else
			{
				season++;
			}
		}

		int round = Mathf.CeilToInt((int)season / 2.0f); //0 ,1 1, 2
		round -= 1; // -1 , 0 , 0 ,1
		round = Mathf.Abs(round); //1,0,0,1
		float rotation = ((float)season + 1) / (float)seasons.Length;

		lightStartIntensity = light.intensity;
		lightTargetIntensity = (1f - (round * 0.5f)) + 0.25f;

		eulerstart = light.transform.localEulerAngles;
		eulerTarget = (new Vector3(eulerstart.x, rotation * 360, 0));

		lightUpdate = 0;
	}

	private void Update()
	{
		//TODo MOVE OUT
		lightUpdate += Time.deltaTime / (seasonTimeInSeconds / 10.0f);
		lightUpdate = Mathf.Min(1, lightUpdate);
		light.transform.localEulerAngles = Vector3.Lerp(eulerstart, eulerTarget, lightUpdate);
		light.intensity = Mathf.Lerp(lightStartIntensity, lightTargetIntensity, lightUpdate);
	}
}
