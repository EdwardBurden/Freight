using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SeasonsController : Singleton<SeasonsController>
{
	//plan better system later
	public float rewardModifier = 0.2f;

	public int minWantAmount = 2;
	public int maxWantAmount = 15;

	public void StartSeason()
	{
		//to test
		foreach (IConsumer consumer in BuildingController.instance.GetAllConsumerBuildings())
		{
			SetConsumerWants(consumer);
		}
	}

	private void SetConsumerWants(IConsumer consumer)
	{
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
		ConsumerWants consumerWants = new ConsumerWants(consumers.ToArray(), rewardAmount);
		consumer.SetWants(consumerWants);
	}
}
