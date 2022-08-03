using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.Events;

[SelectionBase]
public abstract class InteractableObject : MonoBehaviour
{
	[SerializeField]
	private UnityEvent OnHoverEvent;
	[SerializeField]
	private UnityEvent OffHoverEvent;

	public GameObject hoverUIPrefab;
	public bool showHoverUiPanel = false;

	public abstract string GetObjectName();

	public virtual void OffHover()
	{
		OffHoverEvent?.Invoke();
	}

	public virtual void OnHover()
	{
		OnHoverEvent?.Invoke();
	}

}