using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
namespace UI
{
	public class ExitMenuCatcher : MonoBehaviour, IPointerClickHandler
	{
		public void OnPointerClick(PointerEventData pointerEventData)
		{
			if (pointerEventData.button == PointerEventData.InputButton.Left)
			{
				UIManager.instance.CloseAllScreens();
				Debug.Log(name + " BackgroundClickCatcher Clicked!");
			}
			//Output to console the clicked GameObject's name and the following message. You can replace this with your own actions for when clicking the GameObject.

		}
		private void OnMouseDown()
		{
			UIManager.instance.CloseAllScreens();
		}
	}
}