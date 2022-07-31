using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;
namespace UI
{
	public class ShipActionsPanel : Panel
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

		private ShipObject shipObject;

		private ShipActionList shipActionList => shipObject.GetComponent<ShipActionList>();

		private ActionPreview activePreview = null; //maybe move to shipmanager


		protected override void Start()
		{
			base.Start();
			ShipManager.instance.ShipSelectedEvent += OnShipSelected;
			ShipManager.instance.ShipDeSelectedEvent += OnShipDeselected;
		}
		private void OnDestroy()
		{
			ShipManager.instance.ShipSelectedEvent -= OnShipSelected;
			ShipManager.instance.ShipDeSelectedEvent -= OnShipDeselected;
		}

		private void Refresh()
		{
			actionsLeftText.text = $"{shipActionList.GetActionsPushedAmount()}/{shipActionList.maxActions}";
			Clear(actionListTransform);
			SpawnButtons();
			playButton.Init(() => { shipActionList.SetPaused(false); TogglePlayButtons(); });
			pauseButton.Init(() => { shipActionList.SetPaused(true); TogglePlayButtons(); });
			repeatButton.Init(() => { shipActionList.SetRepeat(false); ToggleRepeatButtons(); });
			noRepeatButton.Init(() => { shipActionList.SetRepeat(true); ToggleRepeatButtons(); });
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
			if (activePreview != null)
			{
				activePreview.Close();
				activePreview = null;
			}
			Vector3 pos = selectScreenSpawn.GetComponent<RectTransform>().position;
			ShipActionSelectScreen.Create(pos, shipObject.shipActions, (def) => OnShipActionSelected(def), true);
		}

		private void OnShipActionSelected(ShipActionDefintion shipActionDefintion)
		{
			activePreview = shipActionDefintion.CreateActionPreview(shipObject, ShipManager.instance.PushShipsAction);
		}

		private void Remove(ShipAction shipAction) //shoudl go through ShipManager
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

		private void OnShipSelected(ShipObject shipObject)
		{
			this.shipObject = shipObject;
			shipActionList.ActionStartedEvent += OnActionStarted;
			shipActionList.ActionFinishedEvent += OnActionFinished;
			shipActionList.ActionFailedEvent += OnActionFailed;
			shipActionList.ActionPushedEvent += OnActionPushed;

			Show();
			Refresh();
			//activePreview = null;
		}

		private void OnShipDeselected(ShipObject obj)
		{
			Hide();
			shipActionList.ActionStartedEvent -= OnActionStarted;
			shipActionList.ActionFinishedEvent -= OnActionFinished;
			shipActionList.ActionFailedEvent -= OnActionFailed;
			shipActionList.ActionPushedEvent -= OnActionPushed;
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