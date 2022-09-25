using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class MainMenuScreen : Screen
	{
		public static string PATH = "UI/Screens/MainMenuScreen";

		[SerializeField]
		private Button newGameButton;
		[SerializeField]
		private Button exitButton;

		public static void Create()
		{
			MainMenuScreen screen = UIManager.instance.OpenScreen(PATH, true) as MainMenuScreen;
			screen.Init();
		}

		public void Init()
		{
			newGameButton.onClick.RemoveAllListeners();
			newGameButton.onClick.AddListener(() => MainMenuManager.instance.OnNewGameSelected());

			exitButton.onClick.RemoveAllListeners();
			exitButton.onClick.AddListener(() => MainMenuManager.instance.OnExitGmeSelected());
		}
	}
}