using System;
using System.Collections.Generic;
using UnityEngine;
using UI;
using Screen = UI.Screen;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;

public class UIManager : Singleton<UIManager>
{
	public Canvas loading;

	public IUIController activeController;

	private void Awake()
	{
		_instance = this;
		HideLoading();
	}

	public void SetController(IUIController uIController)
	{
		activeController?.Close();
		activeController = uIController;
		activeController.Open();
	}

	public void ShowLoading()
	{

		loading.enabled = true;
	}

	public void HideLoading()
	{

		loading.enabled = false;
	}


	//swap out active scen ui controller, show loading screens while switching scenes/controllers
}
