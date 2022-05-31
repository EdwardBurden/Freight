using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitBehaviour : CustomBehaviour
{
	public void StartWait(Func<bool> check, Action onSuccess)
	{
		StartCoroutine(WaitCheck(check, onSuccess));

	}

	public void StartWait(int time, Action onSuccess)
	{
		StartCoroutine(WaitTime(time, onSuccess));

	}

	private IEnumerator WaitTime(int time , Action onSuccess) //move into waitbehaviour
	{
		yield return new WaitForSeconds(time);
		onSuccess();
	}

	private IEnumerator WaitCheck(Func<bool> check, Action onSuccess) //move into waitbehaviour
	{
		while (!check.Invoke())
		{
			yield return null;
		}
		onSuccess();
	}


	public override void StopBehaviour()
	{
		StopAllCoroutines();
	}
}
