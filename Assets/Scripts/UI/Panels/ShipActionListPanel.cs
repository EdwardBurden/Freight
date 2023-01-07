using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;
namespace UI
{
	//beig clean up here
	public class ShipActionListPanel : Panel
	{
		[SerializeField]
		private Transform selectScreenSpawn;
		[SerializeField]
		private TMP_Text actionsLeftText;
		[SerializeField]
		private SimpleButtonWidget playButton;
		[SerializeField]
		private SimpleButtonWidget pauseButton;
		[SerializeField]
		private SimpleButtonWidget repeatButton;
		[SerializeField]
		private SimpleButtonWidget noRepeatButton;
		[SerializeField]
		private Sprite addSprite;
		[SerializeField]
		private Transform actionListTransform;
		[SerializeField]
		private ShipActionIconButtonWidget actionButtonWidgetPrefab;
		[SerializeField]
		private SimpleButtonWidget addButtonPrefab;

		private ShipOrderQueue shipActionList;

		private Ship ship => shipActionList.GetComponent<Ship>();

		protected void OnEnable()
		{
			ShipSelectionController.ShipSelectedEvent += OnShipSelected;
			ShipSelectionController.ShipDeSelectedEvent += OnShipDeselected;
		}

		private void OnDisable()
		{
			ShipSelectionController.ShipSelectedEvent -= OnShipSelected;
			ShipSelectionController.ShipDeSelectedEvent -= OnShipDeselected;
		}

		private void Refresh()
		{
			actionsLeftText.text = $"{shipActionList.GetOrderCount()}/{shipActionList.maxOrders}";
			Clear(actionListTransform);
			SpawnButtons();
			playButton.Init(() => { shipActionList.paused = (false); TogglePlayButtons(); });
			pauseButton.Init(() => { shipActionList.paused = (true); TogglePlayButtons(); });
			repeatButton.Init(() => { shipActionList.repeat = (false); ToggleRepeatButtons(); });
			noRepeatButton.Init(() => { shipActionList.repeat = (true); ToggleRepeatButtons(); });
			TogglePlayButtons();
			ToggleRepeatButtons();
		}

		private void TogglePlayButtons()
		{
			playButton.gameObject.SetActive(shipActionList.paused);
			pauseButton.gameObject.SetActive(!shipActionList.paused);
		}

		private void ToggleRepeatButtons()
		{
			repeatButton.gameObject.SetActive(shipActionList.repeat);
			noRepeatButton.gameObject.SetActive(!shipActionList.repeat);
		}

		private void SpawnButtons()
		{
			for (int i = 0; i < shipActionList.maxOrders; i++)
			{
				if (i < shipActionList.allOrders.Count)
				{
					SpawnShipActionIconButton(shipActionList.allOrders[i]);
				}
				else
				{
					SpawnAddbutton(i == shipActionList.allOrders.Count);
				}
			}
		}

		private void SpawnShipActionIconButton(ShipOrder shipAction)
		{
			ShipActionIconButtonWidget actionIcon = Instantiate(actionButtonWidgetPrefab, actionListTransform);
			actionIcon.ShowAction(shipAction.actionDefintion, () => Remove(shipAction));
		}

		private void SpawnAddbutton(bool active)
		{
			SimpleButtonWidget addButton = Instantiate(addButtonPrefab, actionListTransform);
			addButton.Init(Color.white, addSprite, AddClicked, active);
		}

		private void AddClicked()
		{
			ActionPreviewController.instance.DestroyCurrentPreview();
			Vector3 pos = selectScreenSpawn.GetComponent<RectTransform>().position;
			ShipActionSelectScreen.Create(pos, ship.GetShipActions(), (def) => OnShipActionSelected(def), true);
		}

		private void OnShipActionSelected(ShipActionDefintion shipActionDefintion)
		{
			ShipSelectionController shipSelection = FindObjectOfType<ShipSelectionController>();
			shipActionDefintion.CreateActionPreview(ship, shipSelection.PushShipsAction);
		}

		private void Remove(ShipOrder shipAction)
		{
			shipActionList.RemoveOrder(shipAction);
			Refresh();
		}

		private void OnActionPushed(ShipOrder obj)
		{
			Refresh();
		}

		private void OnActionFailed(ShipOrder obj,string message)
		{
			Refresh();
		}

		private void OnActionFinished(ShipOrder obj)
		{
			Refresh();
		}

		private void OnActionStarted(ShipOrder obj)
		{
			Refresh();
		}

		private void OnShipSelected(Ship shipObject)
		{
			Hide();
			ShipOrderQueue shipActionList = shipObject.GetComponent<ShipOrderQueue>();
			if (shipActionList == null)
			{
				return;
			}
			this.shipActionList = shipActionList;
			Show();
		}

		private void OnShipDeselected(Ship obj)
		{
			Hide();
		}

		public override void Show()
		{
			shipActionList.OrderStartedEvent += OnActionStarted;
			shipActionList.OrderFinishedEvent += OnActionFinished;
			shipActionList.OrderFailedEvent += OnActionFailed;
			shipActionList.OrderPushedEvent += OnActionPushed;
			Refresh();
			base.Show();
		}

		public override void Hide()
		{
			base.Hide();
			if (shipActionList != null)
			{
				shipActionList.OrderStartedEvent -= OnActionStarted;
				shipActionList.OrderFinishedEvent -= OnActionFinished;
				shipActionList.OrderFailedEvent -= OnActionFailed;
				shipActionList.OrderPushedEvent -= OnActionPushed;
			}
		}

		protected void Clear(Transform transform)
		{
			foreach (Transform item in transform)
			{
				Destroy(item.gameObject);
			}
		}

	}
}