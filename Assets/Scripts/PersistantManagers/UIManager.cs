using System;
using System.Collections.Generic;
using UnityEngine;
using UI;
using Screen = UI.Screen;

public class UIManager : Singleton<UIManager>
{
	[SerializeReference]
	public Canvas inScene;
	[SerializeReference]
	public Canvas HUD;
	[SerializeReference]
	public Canvas panels;
	[SerializeReference]
	public Canvas screens;
	[SerializeReference]
	private GameObject backgroundClickCatcher;

	public List<Screen> screenHistory = new List<Screen>();

	private void Awake()
	{
		_instance = this;
		ClearAllUI();
	}


	public void ClearAllUI()
	{
		GameObjectUtils.Clear(panels.transform);
		CloseAllScreens();
		DisableHUD();
	}


	//Hud
	public void EnableHUD()
	{
		HUD.gameObject.SetActive(true);

	}

	public void DisableHUD()
	{
		HUD.gameObject.SetActive(false);
	}


	//Panel
	internal void AddPanels(List<Panel> gameUIPanels)
	{
		foreach (Panel panelPrefab in gameUIPanels)
		{
			AddPanel(panelPrefab);
		}

	}

	public void AddPanel(Panel prefab) //called from 
	{
		Panel panel = Instantiate(prefab, panels.transform).GetComponent<Panel>();
	}


	//InScene
	public InScene CreateInScene(InScene prefab, Transform transform)
	{
		InScene inSceneUI = Instantiate(prefab, inScene.transform).GetComponent<InScene>();
		inSceneUI.SetFollow(transform);
		return inSceneUI;
	}


	//Screens
	public Screen OpenScreen(string path, bool closeOthers = true)
	{
		Screen screen = Instantiate(Resources.Load<GameObject>(path), screens.transform).GetComponent<Screen>();
		RegisterScreen(screen, closeOthers);
		return screen;
	}

	private void RegisterScreen(Screen screen, bool closeOthers)
	{
		if (closeOthers)
		{
			CloseAllScreens();
		}

		if (screenHistory.Find(x => x.GetType() == screen.GetType()))
		{
			Debug.LogError("two panel of same type");
			return;
		}
		screenHistory.Add(screen);
		backgroundClickCatcher.SetActive(true);
	}

	public void CloseAllScreens()
	{
		for (int i = screenHistory.Count - 1; i >= 0; i--)
		{
			CloseScreen(screenHistory[i]);
		}
	}

	public void CloseScreen(Screen screen)
	{
		Destroy(screen.gameObject);
		screenHistory.Remove(screen);
		TryCloseBackgroundScreen();
	}

	public void BackOneScreen()
	{
		int index = screenHistory.Count - 1;
		CloseScreen(screenHistory[index]);
	}

	private void TryCloseBackgroundScreen()
	{
		if (screenHistory.Count == 0)
		{
			backgroundClickCatcher.SetActive(false);
		}
	}
}
