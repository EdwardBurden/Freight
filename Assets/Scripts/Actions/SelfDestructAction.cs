using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructAction : GameAction
{
	public float timer;
	public float timeRemaining;

	public SelfDestructAction(float timetodestory, Orderable orderable, GameActionDefintion defintion) : base(orderable, defintion)
	{
		this.timer = timetodestory;
	}

	public override void OrderStarted()
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

	public override void OrderUpdated()
	{
		timeRemaining -= Time.deltaTime;
		if (timeRemaining < 0) {
			CompleteOrder();
		}
	}
}
