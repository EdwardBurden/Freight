using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ActionDefintion : ScriptableObject
{
	public GameObject actionButtonPrefab;
	public string Name;
	public string description;
	public Sprite icon;

	//can be shown as option or right click
	public abstract bool ActionAvailable(ControllableObject controllableObject, InteractableObject interactableObject, Vector3 clickPostion);


	public abstract void OnActionSelected(ControllableObject controllableObject, InteractableObject interactableObject, Vector3 clickPostion);


	public GameObject CreateButton(Transform menuTransfrom)
	{
		GameObject gameObject = Instantiate(actionButtonPrefab, menuTransfrom);
		TMP_Text text = gameObject.GetComponentInChildren<TMP_Text>();
		Image image = gameObject.transform.GetChild(0).GetComponent<Image>();

		text.text = name;
		image.sprite = icon;
		return gameObject;
	}
}