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
	}


	private void TEST_SetConsumerWants(IConsumer consumer)
	{
		float rewardModifier = 0.2f;
		int minWantAmount = 2;
		int maxWantAmount = 15;



		ResourceDefintion[] resources = consumer.GetAcceptedResources();
		List<ConsumerWant> consumers = new List<ConsumerWant>();
		float rewardAmount = 0;

		for (int i = 0; i < resources.Length; i++)
		{
			float chance = Random.Range(0f, 1f);
			if (chance > 0.5f)
			{
				int amount = Random.Range(minWantAmount, maxWantAmount);
				consumers.Add(new ConsumerWant(resources[i], amount));
				rewardAmount += resources[i].sellPrice;
			}
		}
		rewardAmount *= rewardModifier;
		SettlementWants consumerWants = new SettlementWants(consumers.ToArray(), rewardAmount);
		consumer.SetWants(consumerWants);
	}
}
