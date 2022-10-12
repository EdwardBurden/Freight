using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

//controls the main menu scene, opens main menu ui and controls flow
public class MainMenuManager : Singleton<MainMenuManager>
{

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
		UIManager.instance.SetCamera(Camera.main);
		MainMenuScreen.Create();// ie UIManager.instance.OpenScreen(mainmenu)
	}

	public void OnNewGameSelected()
	{
		UIManager.instance.CloseAllScreens();
		AppManager.instance.OpenGameScene();
	}

	public void OnExitGmeSelected()
	{
		AppManager.instance.CloseApplication();
	}

}
