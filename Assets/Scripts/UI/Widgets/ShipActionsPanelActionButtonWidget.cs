using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	//Used by the ShipActionsPanel as buttons that hodl a action and can be swapped out to select an action
	public class ShipActionsPanelActionButtonWidget : MonoBehaviour
	{
		[SerializeField]
		private Button addButton;

		[SerializeField]
		private Transform actionIconSpawn;


		internal void ShowAction(ShipAction shipAction)
		{
			//spawn action icon, setuop remove button
			addButton.gameObject.SetActive(false);
			actionIconSpawn.gameObject.SetActive(true);
		}

		internal void ShowAdd(Action action)
		{
			addButton.gameObject.SetActive(true);
			actionIconSpawn.gameObject.SetActive(false);
			addButton.onClick.RemoveAllListeners();
			addButton.onClick.AddListener(() => action());
		}

		//[SerializeField]
		// private Transform 

	}
}