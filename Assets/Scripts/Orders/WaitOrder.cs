using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitOrder : Order //WaitForeseconds for testing order work in order for now
{
	private int waitTime;
	public WaitOrder(int waitTime, Orderable orderable) : base(orderable)
	{
		this.waitTime = waitTime;
	}

	public override void BeginOrder()
	{
		orderable.StartCoroutine(Wait());
	}

	public override void CancelOrder()
	{
		throw new System.NotImplementedException();
	}

	public override void EndOrder()
	{
		orderable.StopCoroutine(Wait());
	}

	private IEnumerator Wait() //move into waitbehaviour
	{
		yield return new WaitForSeconds(waitTime);
		Debug.LogError("waited");
		OrderComplete();
		yield return null;
	}
}
