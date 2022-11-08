using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppManager : Singleton<AppManager>
{
	private void Awake()
	{
		_instance = this;
	}


	internal void CloseApplication()
	{
		Application.Quit();
	}
}
