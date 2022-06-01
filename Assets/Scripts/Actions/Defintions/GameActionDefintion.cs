using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class GameActionDefintion : ScriptableObject
{
	public GameObject actionButtonPrefab;
	public string actionName;
	public string description;
	public Sprite icon;

	//can be shown as option or right click
	public abstract bool ActionAvailable(ControllableObject controllableObject, InteractableObject interactableObject, Vector3 clickPostion);


	public abstract void OnActionSelected(ControllableObject controllableObject, InteractableObject interactableObject, Vector3 clickPostion);


	public virtual GameObject CreateButton(Transform menuTransfrom)
	{
		GameObject gameObject = Instantiate(actionButtonPrefab, menuTransfrom);
		InteractionButton interactionButton = gameObject.GetComponent<InteractionButton>();
		interactionButton.Init(actionName, icon);
		return gameObject;
	}
}