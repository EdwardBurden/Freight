using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;
namespace UI
{
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

		private ShipActionList shipActionList;

		private Ship ship => shipActionList.GetComponent<Ship>();


		protected void Start()
		{
			ShipSelectionController.ShipSelectedEvent += OnShipSelected;
			ShipSelectionController.ShipDeSelectedEvent += OnShipDeselected;
		}
		private void OnDestroy()
		{
			ShipSelectionController.ShipSelectedEvent -= OnShipSelected;
			ShipSelectionController.ShipDeSelectedEvent -= OnShipDeselected;
		}

		private void Refresh()
		{
			actionsLeftText.text = $"{shipActionList.GetActionsCount()}/{shipActionList.maxActions}";
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
			for (int i = 0; i < shipActionList.maxActions; i++)
			{
				if (i < shipActionList.actions.Count)
				{
					SpawnShipActionIconButton(shipActionList.actions[i]);
				}
				else
				{
					SpawnAddbutton(i == shipActionList.actions.Count);
				}
			}
		}

		private void SpawnShipActionIconButton(ShipAction shipAction)
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
			ActionPreviewController.instance.RemoveActionPreview();
			Vector3 pos = selectScreenSpawn.GetComponent<RectTransform>().position;
			ShipActionSelectScreen.Create(pos, ship.shipActions, (def) => OnShipActionSelected(def), true);
		}

		private void OnShipActionSelected(ShipActionDefintion shipActionDefintion)
		{
			ShipSelectionController shipSelection = FindObjectOfType<ShipSelectionController>();
			shipActionDefintion.CreateActionPreview(ship, shipSelection.PushShipsAction);
		}

		private void Remove(ShipAction shipAction)
		{
			shipActionList.RemoveAction(shipAction);
			Refresh();
		}


		private void OnActionPushed(ShipAction obj)
		{
			Refresh();
		}

		private void OnActionFailed(ShipAction obj)
		{
			Refresh();
		}

		private void OnActionFinished(ShipAction obj)
		{
			Refresh();
		}

		private void OnActionStarted(ShipAction obj)
		{
			Refresh();
		}

		private void OnShipSelected(Ship shipObject)
		{
			Hide();
			ShipActionList shipActionList = shipObject.GetComponent<ShipActionList>();
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
			shipActionList.ActionStartedEvent += OnActionStarted;
			shipActionList.ActionFinishedEvent += OnActionFinished;
			shipActionList.ActionFailedEvent += OnActionFailed;
			shipActionList.ActionPushedEvent += OnActionPushed;
			Refresh();
			base.Show();
		}

		public override void Hide()
		{
			base.Hide();
			if (shipActionList != null)
			{
				shipActionList.ActionStartedEvent -= OnActionStarted;
				shipActionList.ActionFinishedEvent -= OnActionFinished;
				shipActionList.ActionFailedEvent -= OnActionFailed;
				shipActionList.ActionPushedEvent -= OnActionPushed;
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