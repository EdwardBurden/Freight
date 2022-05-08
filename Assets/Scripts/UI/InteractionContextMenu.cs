using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionContextMenu : MonoBehaviour
{
	public GameObject actionPrefab;
	public Transform menuTransfrom;

	private void Awake()
	{
		Close();
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
		Vector3 hit = Camera.main.WorldToScreenPoint(positionInWorld);
		menuTransfrom.position = hit;
		menuTransfrom.gameObject.SetActive(true);
	}

	internal void Close()
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
}
