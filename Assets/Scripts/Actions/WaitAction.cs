using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitAction : GameAction //WaitForeseconds for testing order work in order for now
{
	WaitBehaviour waitBehaviour;
	private int waitTime;

	public WaitAction(int waitTime, Orderable orderable, GameActionDefintion actionDefintion) : base(orderable, actionDefintion)
	{
		waitBehaviour = orderable.GetComponent<WaitBehaviour>();
		this.waitTime = waitTime;
	}

	public override void OrderStarted()
	{
		waitBehaviour.StartWait(waitTime, CompleteOrder);
	}

	public override void OrderCancelled()
	{
		waitBehaviour.StopBehaviour();
	}

	public override void OrderFinished()
	{
		waitBehaviour.StopBehaviour();
	}

	public override void OrderUpdated()
	{
		//
	}
}
