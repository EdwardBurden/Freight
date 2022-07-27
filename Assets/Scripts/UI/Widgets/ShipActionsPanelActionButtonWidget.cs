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

		internal void Show(ShipAction shipAction)
		{

			transform.GetChild(0).gameObject.SetActive(shipAction == null);
			transform.GetChild(1).gameObject.SetActive(shipAction != null);
		}

		//[SerializeField]
		// private Transform 

	}
}