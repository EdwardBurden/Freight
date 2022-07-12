using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[SelectionBase]
public class InteractableObject : MonoBehaviour
{
	//use events for highlights etc
	[SerializeField]
	private UnityEvent OnHoverEvent;
	[SerializeField]
	private UnityEvent OffHoverEvent;

	public virtual void OffHover()
	{
		OffHoverEvent?.Invoke();
	}

	public virtual void OnHover()
	{
		OnHoverEvent?.Invoke();
	}

}