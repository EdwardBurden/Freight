using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitAction : FreightAction //WaitForeseconds for testing order work in order for now
{
	private int waitTime;
	public WaitAction(int waitTime, Orderable orderable, ActionDefintion actionDefintion) : base(orderable, actionDefintion)
	{
		this.waitTime = waitTime;
	}

	public override void BeginOrder()
	{
		orderable.StartCoroutine(Wait());
	}

	public override void OrderCancelled()
	{
		throw new System.NotImplementedException();
	}

	public override void OrderFinished()
	{
		orderable.StopCoroutine(Wait());
	}

	public override void UpdateOrder()
	{
		throw new System.NotImplementedException();
	}

	private IEnumerator Wait() //move into waitbehaviour
	{
		yield return new WaitForSeconds(waitTime);
		Debug.LogError("waited");
		CompleteOrder();
		yield return null;
	}
}
