using System;
using System.Collections.Generic;
using UnityEngine;
using UI;
using Screen = UI.Screen;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;

public class UIManager : Singleton<UIManager>
{
	[SerializeReference]
	public Camera uiCamera;
	[SerializeReference]
	private Canvas inScene;
	[SerializeReference]
	private Canvas HUD;
	[SerializeReference]
	private Canvas panels;
	[SerializeReference]
	private Canvas screens;
	[SerializeReference]
	private GameObject backgroundClickCatcher;

	public Vector2 GetInScenePosition(Vector3 position)
	{
		return UIUtils.GetAnchoredPostionFromWorldSpacePoint(position, inScene);
	}

	public Vector2 GetScreenPosition(Vector3 position)
	{
		return UIUtils.GetAnchoredPostionFromWorldSpacePoint(position, screens);
	}



	public List<Screen> screenHistory = new List<Screen>();

	private void Awake()
	{
		_instance = this;
		ToggleHUD(false);
	}

	public void SetCamera(Camera camera)
	{
		var cameraData = camera.GetUniversalAdditionalCameraData();
		cameraData.cameraStack.Add(uiCamera);
	}

	public void ClearAllUI()
	{
		GameObjectUtils.Clear(panels.transform);
		CloseAllScreens();
		ToggleHUD(false);
	}


	//Hud
	public void ToggleHUD(bool active)
	{
		HUD.gameObject.SetActive(active);
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
