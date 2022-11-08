using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.Rendering.Universal;
namespace UI
{

	public class MainMenuUIController : MonoBehaviour, IUIController
	{
		[SerializeReference]
		public Camera uiCamera;
		public Canvas menus;

		private void Awake()
		{
			Close();
		}

		public void Close()
		{
			uiCamera.enabled = false;
			menus.enabled = false;
		}

		public void Open()
		{
			uiCamera.enabled = true;
			menus.enabled = true;
		}
	}
}
