using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

//controls the main menu scene, opens main menu ui and controls flow
public class MainMenuSceneController : Singleton<MainMenuSceneController>
{
	[SerializeField] public MainMenuUIController mainMenuUIController;

	private void Awake()
	{
		_instance = this;
	}

	private void Start()
	{
		OnSceneOpen();
	}

	public void OnSceneOpen()
	{
		//set up background?
	}

	public void OnNewGameSelected()
	{
		AppManager.instance.OpenGameScene();
	}

	public void OnExitGmeSelected()
	{
		AppManager.instance.CloseApplication();
	}

}
