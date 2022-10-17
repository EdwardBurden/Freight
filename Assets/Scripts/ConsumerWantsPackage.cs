using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlementWants 
{
	public ConsumerWant[] consumerWants;
	public float rewardAmount;

	public SettlementWants(ConsumerWant[] consumerWants, float rewardAmount)
	{
		this.consumerWants = consumerWants;
		this.rewardAmount = rewardAmount;
	}
}
