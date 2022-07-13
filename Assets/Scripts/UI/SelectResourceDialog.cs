using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectResourceDialog : MonoBehaviour
{
	private Action<ResourceData> resourceSelectedCallback;


	public void Open(List<ResourceData> resourceDatas, Action<ResourceData> callback)
	{
		resourceSelectedCallback = callback;

		//temp for testing
		resourceSelectedCallback?.Invoke(resourceDatas[0]);
	}
}
