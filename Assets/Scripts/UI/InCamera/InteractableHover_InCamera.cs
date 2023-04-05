using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
	public class InteractableHover_InCamera : MonoBehaviour
	{
		[SerializeField] private TMP_Text nameText;

		private InteractableObject interactableObject;

		public void ShowName(InteractableObject interactableObject)
		{
			nameText.text = interactableObject.GetInteractableObjectName();
		}

		private void Update()
		{
			Vector2 movePos;
			Canvas canvas = this.transform.parent.GetComponent<Canvas>();

			RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), Mouse.current.position.ReadValue(), canvas.worldCamera, out movePos);

			transform.position = canvas.transform.TransformPoint(movePos);
		}
	}
}