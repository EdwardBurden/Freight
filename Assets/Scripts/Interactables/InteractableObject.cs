using UI;
using UnityEngine;

[SelectionBase]
public abstract class InteractableObject : MonoBehaviour
{
	[SerializeField]
	private InteractableHover_InCamera hoverUIPrefab; //todo move out to new hover/highlight controller, use same hover prefab for everything

	private InteractableHover_InCamera hoverUI;

	public abstract string GetInteractableObjectName();

	public virtual void OffHover()
	{
		if (hoverUI != null)
		{
			Destroy(hoverUI.gameObject);
		}
	}

	public virtual void OnHover()
	{
		if (hoverUIPrefab != null)
		{
			hoverUI = GameUIController.instance.CreateInScene(hoverUIPrefab.gameObject, this.transform).GetComponent<InteractableHover_InCamera>();
			hoverUI.ShowName(this);
		}
	}
}