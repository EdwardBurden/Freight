using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.Events;

[SelectionBase]
public abstract class InteractableObject : MonoBehaviour
{
	[SerializeField]
	private InScene hoverUIPrefab;
	public bool showHoverInfo;

	private InteractableHoverInScene hoverUI;

	public abstract string GetObjectName();

	public virtual void OffHover()
	{
		if (hoverUI != null)
		{
			Destroy(hoverUI.gameObject);
		}
	}

	public virtual void OnHover()
	{
		if (showHoverInfo)
		{
			hoverUI = GameUIManager.instance.CreateInScene(hoverUIPrefab, this.transform) as InteractableHoverInScene;
			hoverUI.OnHover(this);
		}
	}

}