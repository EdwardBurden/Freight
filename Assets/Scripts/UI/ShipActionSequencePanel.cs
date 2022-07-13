using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UI
{
	//show list of actions , updates as actions finish
	public class ShipActionSequencePanel : MonoBehaviour
	{
		[SerializeField]
		private GameObject root;

		[SerializeField]
		private ShipActionIconWidget shipActionIconWidgetPrefab;


		private void Awake()
		{
			ShipManager.ShipSelectedEvent += ShipManager_ShipSelectedEvent;
			ShipManager.ShipDeSelectedEvent += ShipManager_ShipDeSelectedEvent;
			//actionmanager += action added
			//actionmanager += action complete
			//actionmanager += action failed

		}

		private void Start()
		{
			Hide();
		}

		private void OnDestroy()
		{
			ShipManager.ShipSelectedEvent -= ShipManager_ShipSelectedEvent;
			ShipManager.ShipDeSelectedEvent -= ShipManager_ShipDeSelectedEvent;
		}

		private void ShipManager_ShipDeSelectedEvent(ShipObject obj)
		{
			Hide();
		}

		private void ShipManager_ShipSelectedEvent(ShipObject obj)
		{
			Show();
		}

		public void Show()
		{
			root.SetActive(true);
		}

		public void Hide()
		{
			root.SetActive(false);
		}

		private void Refresh(ShipObject shipObject)
		{
			shipObject.GetActionList();

			//foreach action
			//getdefintion
			//spawn widget
			//pass through action


		}

		//open
		//close
		//onshipselected event subscribe
		//onshipdeselected event subscribe
	}
}