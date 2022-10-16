using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumerWants 
{
	public ConsumerWant[] consumerWants;
	public float rewardAmount;

	public ConsumerWants(ConsumerWant[] consumerWants, float rewardAmount)
	{
		this.consumerWants = consumerWants;
		this.rewardAmount = rewardAmount;
	}
}
