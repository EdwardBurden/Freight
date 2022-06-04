using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using TMPro;

public class InteractionContextMenu : Menu
{
	[SerializeField]
	private GameObject actionPrefab;

	private void Awake()
	{
		RegisterEvents();
		Close();
	}

	public void DisplayOptions(Vector3 positionInWorld, GameActionDefintion[] actions)
	{
		Clear();
		Open(positionInWorld);
		foreach (GameActionDefintion action in actions)
		{
			GameObject gameObject = action.CreateButton(menuTransfrom);
			Button button = gameObject.GetComponent<Button>();
			button.onClick.AddListener(() => OptionSelected(action));
		}

	}

	private void OptionSelected(GameActionDefintion interactionOrder)
	{
		Close();
		interactionOrder.OnActionSelected(ControlledObjectsInteractionManager.controlledObject, ControlledObjectsInteractionManager.targetInteractableObject, ControlledObjectsInteractionManager.clickPosition);
	}

	private void Open(Vector3 positionInWorld)
	{
		Vector3 hit = Camera.main.WorldToScreenPoint(positionInWorld);
		menuTransfrom.position = hit;
		Open();
	}
}
