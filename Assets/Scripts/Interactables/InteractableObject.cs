using UI;
using UnityEngine;

[SelectionBase]
public abstract class InteractableObject : MonoBehaviour
{
	[SerializeField]
	private InteractableHover_InCamera hoverUIPrefab;

	private InteractableHover_InCamera hoverUI;

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
		if (hoverUIPrefab != null)
		{
			hoverUI = GameUIController.instance.CreateInScene(hoverUIPrefab, this.transform) as InteractableHover_InCamera;
			hoverUI.ShowName(this);
		}
	}
}