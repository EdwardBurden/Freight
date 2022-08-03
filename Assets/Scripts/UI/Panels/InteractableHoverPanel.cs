using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
namespace UI
{
	public class InteractableHoverPanel : Panel
	{
		public TMP_Text nameText;

		[SerializeField]
		private Transform body;

		protected override void Start()
		{
			base.Start();
			InputManager.instance.mouseHoveredOnInteractable += Instance_mouseHoveredOnInteractable;
			InputManager.instance.mouseHoveredOffInteractable += Instance_mouseHoveredOffInteractable;
			//MUImanager on hover something in ui
		}

		private void Instance_mouseHoveredOffInteractable(InteractableObject obj)
		{
			Hide();
		}

		private void Instance_mouseHoveredOnInteractable(InteractableObject obj)
		{
			if (obj.hoverUIPrefab == null)
			{
				//Or draw generic
				return;
			}

			Show();
			Clear(body);
			Instantiate(obj.hoverUIPrefab, body);
			nameText.text = obj.GetObjectName();
		}

		private void OnDestroy()
		{
			InputManager.instance.mouseHoveredOnInteractable -= Instance_mouseHoveredOnInteractable;
			InputManager.instance.mouseHoveredOffInteractable -= Instance_mouseHoveredOffInteractable;
		}
		protected void Clear(Transform transform)
		{
			foreach (Transform item in transform)
			{
				Destroy(item.gameObject);
			}
		}
	}
}