using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
	public Transform menuTransfrom;
	private void Awake()
	{
		RegisterEvents();
		Close();
	}

	protected virtual void RegisterEvents()
	{

	}

	public bool IsMenuOpen()
	{
		return menuTransfrom.gameObject.activeInHierarchy;
	}

	public void Close()
	{
		Clear();
		menuTransfrom.gameObject.SetActive(false);
	}

	protected void Clear()
	{
		foreach (Transform child in menuTransfrom.transform)
		{
			GameObject.Destroy(child.gameObject);
		}
	}

	public void Open()
	{
		menuTransfrom.gameObject.SetActive(true);
	}
}
