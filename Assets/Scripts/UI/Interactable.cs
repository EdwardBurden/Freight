using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//anything the user can hover over, click, maybe control , should open ui or play sound when hovering/clicked
public class Interactable : MonoBehaviour
{
	[SerializeField]
	private InteractableBillboard interactableBillboardPrefab;
	[SerializeField]
	private Canvas billBoardCanvas;


	private InteractableBillboard interactableBillboard;

	public void OnEnable()
	{
		CreateUIBillboard();
	}

	private void OnDisable()
	{
		if (interactableBillboard != null)
		{
			Destroy(interactableBillboard.gameObject);
		}
	}

	private void CreateUIBillboard()
	{
		interactableBillboard = Instantiate(interactableBillboardPrefab, billBoardCanvas.transform);
	}
}
