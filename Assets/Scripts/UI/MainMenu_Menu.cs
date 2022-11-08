using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class MainMenu_Menu : MonoBehaviour
	{
		[SerializeField]
		private Button newGameButton;
		[SerializeField]
		private Button exitButton;

		private void OnEnable()
		{
			newGameButton.onClick.AddListener(() => MainMenuSceneController.instance.OnNewGameSelected());
			exitButton.onClick.AddListener(() => MainMenuSceneController.instance.OnExitGmeSelected());
		}

		public void OnDisable()
		{
			newGameButton.onClick.RemoveAllListeners();
			exitButton.onClick.RemoveAllListeners();
		}
	}
}