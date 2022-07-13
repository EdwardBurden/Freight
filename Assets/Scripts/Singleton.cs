using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour
{
	protected static T _instance;

	public static T instance
	{
		get
		{
			if (_instance == null)
			{
				Debug.LogError($"{typeof(T)} missing");
			}
			return _instance;
		}
	}


}
