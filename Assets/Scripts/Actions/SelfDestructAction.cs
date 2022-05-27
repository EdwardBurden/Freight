using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructAction : FreightAction
{
	public float timer;
	public float timeRemaining;

	public SelfDestructAction(float timetodestory, Orderable orderable, ActionDefintion defintion) : base(orderable, defintion)
	{
		this.timer = timetodestory;
	}

	public override void BeginOrder()
	{
		timeRemaining = timer;
	}

	public override void OrderCancelled()
	{
		//throw new System.NotImplementedException();
	}

	public override void OrderFinished()
	{
		GameObject.Destroy(orderable.gameObject);
		//throw new System.NotImplementedException();
	}

	public override void UpdateOrder()
	{
		timeRemaining -= Time.deltaTime;
		if (timeRemaining < 0) {
			CompleteOrder();
		}
	}
}
