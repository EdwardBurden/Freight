using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using TMPro;

public class InteractionContextMenu : MonoBehaviour
{
	public GameObject actionPrefab;
	public Transform menuTransfrom;

	public static event Action<InteractionOrderDefintion> optionSelected;

	private void Awake()
	{
		RegisterEvents();
		Close();
	}

	private void RegisterEvents()
	{
		//EventSystem.current.SetSelectedGameObject(menuTransfrom.gameObject);
	}

	public void DisplayOptions(Vector3 positionInWorld, List<InteractionOrderDefintion> interactionOrderDefintions)
	{
		Clear();
		Open(positionInWorld);
		foreach (InteractionOrderDefintion interactionOrder in interactionOrderDefintions)
		{
			GameObject gameObject = Instantiate(actionPrefab, menuTransfrom);
			Button button = gameObject.GetComponent<Button>();
			TMP_Text text = gameObject.GetComponentInChildren<TMP_Text>();
			text.text = interactionOrder.orderName;
			button.onClick.AddListener(() => OptionSelected(interactionOrder));
		}

	}

	public bool IsMenuOpen()
	{
		return menuTransfrom.gameObject.activeInHierarchy;
	}

	private void OptionSelected(InteractionOrderDefintion interactionOrder)
	{
		Close();
		optionSelected?.Invoke(interactionOrder);
	}

	private void Open(Vector3 positionInWorld)
	{
		Vector3 hit = Camera.main.WorldToScreenPoint(positionInWorld);
		menuTransfrom.position = hit;
		menuTransfrom.gameObject.SetActive(true);
	}

	public void Close()
	{
		Clear();
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
