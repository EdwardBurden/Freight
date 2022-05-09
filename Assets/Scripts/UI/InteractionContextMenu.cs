using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractionContextMenu : MonoBehaviour
{
	public GameObject actionPrefab;
	public Transform menuTransfrom;

	private void Awake()
	{
		RegisterEvents();
		Close();
	}

	private void RegisterEvents()
	{
		EventSystem.current.SetSelectedGameObject(menuTransfrom.gameObject);
	}

	internal void DisplayOptions(string[] actionNames)
	{
		Clear();
		foreach (var item in actionNames)
		{
			Instantiate(actionPrefab, menuTransfrom);
		}
	}

	internal bool IsMenuOpen()
	{
		return menuTransfrom.gameObject.activeInHierarchy;
	}

	internal void Open(Vector3 positionInWorld)
	{
		RegisterEvents();
		   Vector3 hit = Camera.main.WorldToScreenPoint(positionInWorld);
		menuTransfrom.position = hit;
		menuTransfrom.gameObject.SetActive(true);
	}

	private void Close()
	{
		menuTransfrom.gameObject.SetActive(false);
	}

	private void Clear()
	{
		foreach (Transform child in menuTransfrom.transform)
		{
			GameObject.Destroy(child.gameObject);
		}
	}

	public void OnDeselect(BaseEventData eventData)
	{
		Close();
		Debug.LogError("");
		//Mouse was clicked outside
	}
}
