using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BackgroundClickCatcher : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
	public UnityEvent OnClickEvent;
	public static bool mouseOver;

	public void OnPointerClick(PointerEventData pointerEventData)
	{
		OnClickEvent?.Invoke();
		//Output to console the clicked GameObject's name and the following message. You can replace this with your own actions for when clicking the GameObject.
		Debug.Log(name + " Game Object Clicked!");
	}
	private void OnMouseDown()
	{
		OnClickEvent?.Invoke();
	}

	private void OnMouseOver()
	{
		mouseOver = true;
	}



	public void OnPointerEnter(PointerEventData eventData)
	{
		mouseOver = true;
		Debug.Log(name + " Game Object enter!");
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		mouseOver = false;
		Debug.Log(name + " Game Object exit!");
	}
}
