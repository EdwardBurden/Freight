using System.Collections.Generic;
using UnityEngine;
namespace UI
{
	public class UIManager : Singleton<UIManager>
	{
		public Transform panelsTransform;
		public Transform screentransform;
		public GameObject backgroundClickCatcher;
		//money
		//orders
		//ship info
		//building info
		//reflects something in game very directly
		public List<Panel> activePanels = new List<Panel>(); //add/ remove when necassry stays as long as they want, one one of the a panel type

		public List<Screen> screenHistory = new List<Screen>(); //only one screeen at a time, takes up large screen space, history so that they can be backed out of  


		private void Awake()
		{
			_instance = this;
		}

		public void RegisterScreen(Screen screen, bool closeOthers = true)
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
			screen.Close();
			screenHistory.Remove(screen);
			TryCloseBackground();
		}

		public void BackScreen()
		{
			int index = screenHistory.Count - 1;
			screenHistory[index].Close();
			screenHistory.RemoveAt(index);
			TryCloseBackground();
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