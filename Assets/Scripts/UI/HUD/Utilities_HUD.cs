using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{

	public class Utilities_HUD : HUD
	{
		[SerializeField] private Button settingsButton;

		private void OnEnable()
		{
			settingsButton.onClick.AddListener(SettingsClicked);
		}

		private void OnDisable()
		{
			settingsButton.onClick.RemoveAllListeners();
		}

		private void SettingsClicked()
		{
			GameManager.instance.EndGame();
			GameManager.instance.GoToMainMenu();


		}
	}
}