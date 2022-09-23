using System;
using System.Collections.Generic;
using UnityEngine;
namespace UI
{
	public class GameUIManager : Singleton<GameUIManager>
	{
		public Canvas inSceneCanvas;
		public Canvas panels;
		public Canvas screens;
		public GameObject backgroundClickCatcher;

		public List<Screen> screenHistory = new List<Screen>(); //only one screeen at a time, takes up large screen space, history so that they can be backed out of  

		public InScene CreateInScene(InScene prefab, Transform transform)
		{
			InScene inScene = Instantiate(prefab, inSceneCanvas.transform).GetComponent<InScene>();
			inScene.SetFollow(transform);
			return inScene;
		}

		private void Awake()
		{
			_instance = this;
		}

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
				Close(screenHistory[i]);
			}
		}

		public void Close(Screen screen)
		{
			Destroy(screen.gameObject);
			screenHistory.Remove(screen);
			TryCloseBackground();
		}

		public void Back()
		{
			int index = screenHistory.Count - 1;
			Close(screenHistory[index]);
		}

		private void TryCloseBackground()
		{
			if (screenHistory.Count == 0)
			{
				backgroundClickCatcher.SetActive(false);
			}
		}

	}
}