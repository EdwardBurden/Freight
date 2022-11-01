using UI;
using UnityEngine;

[SelectionBase]
public abstract class InteractableObject : MonoBehaviour
{
	[SerializeField]
	private InScene hoverUIPrefab;

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
		if (hoverUIPrefab != null)
		{
			hoverUI = UIManager.instance.CreateInScene(hoverUIPrefab, this.transform) as InteractableHoverInScene;
			hoverUI.OnHover(this);
		}
	}
}