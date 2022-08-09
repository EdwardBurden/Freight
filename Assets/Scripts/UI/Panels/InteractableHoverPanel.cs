using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
namespace UI
{
	public class InteractableHoverPanel : Panel //shoudl be in game now
	{
		[SerializeField]
		private TMP_Text nameText;

		[SerializeField]
		private Transform body;

		private InteractableObject interactableObject;

		protected override void Start()
		{
			base.Start();
			GameInputManager.instance.mouseHoveredOnInteractable += Instance_mouseHoveredOnInteractable;
			GameInputManager.instance.mouseHoveredOffInteractable += Instance_mouseHoveredOffInteractable;
			//MUImanager on hover something in ui
		}

		private void Instance_mouseHoveredOffInteractable(InteractableObject obj)
		{
			Hide();
			interactableObject = null;
		}

		private void Instance_mouseHoveredOnInteractable(InteractableObject obj)
		{
			if (obj.hoverUIPrefab == null)
			{
				//Or draw generic
				return;
			}
			interactableObject = obj;
			Show();
			Clear(body);
			Instantiate(obj.hoverUIPrefab, body);
			nameText.text = obj.GetObjectName();
		}

		private void OnDestroy()
		{
			GameInputManager.instance.mouseHoveredOnInteractable -= Instance_mouseHoveredOnInteractable;
			GameInputManager.instance.mouseHoveredOffInteractable -= Instance_mouseHoveredOffInteractable;
		}
		protected void Clear(Transform transform)
		{
			foreach (Transform item in transform)
			{
				Destroy(item.gameObject);
			}
		}

		private void Update()
		{
			if (interactableObject != null)
			{
				root.transform.GetComponent<RectTransform>().anchoredPosition = UIUtils.GetAnchoredPostionFromWorldSpacePoint(interactableObject.transform.position, GameUIManager.instance.panels);
			}
		}
	}
}