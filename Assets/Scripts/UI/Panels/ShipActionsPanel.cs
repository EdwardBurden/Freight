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
		private TMP_Text shipNametext;
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
		private Transform actionListTransform;

		[SerializeField]
		private ShipActionsPanelActionButtonWidget buttonWidget; //re use for now, replace with dragbbl one later

		private ShipObject shipObject;
		private ShipActionList shipActionList => shipObject.GetComponent<ShipActionList>();

		protected override void Start()
		{
			base.Start();
			ShipSelectionManager.instance.ShipSelectedEvent += OnShipSelected;
			ShipSelectionManager.instance.ShipDeSelectedEvent += OnShipDeselected;
		}
		private void OnDestroy()
		{
			ShipSelectionManager.instance.ShipSelectedEvent -= OnShipSelected;
			ShipSelectionManager.instance.ShipDeSelectedEvent -= OnShipDeselected;
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
				ShipActionsPanelActionButtonWidget actionIcon = Instantiate(buttonWidget, actionListTransform);
				if (i < shipActionList.actions.Count)
				{
					ShipAction shipAction = shipActionList.actions[i];
					actionIcon.ShowAction(shipAction);
				}
				else
				{
					//SelectActionForPanelScreen.Create(shipObject.shipActions, (def) => OnShipActionSelected(def));
					actionIcon.ShowAdd(() => SelectActionForPanelScreen.Create(shipObject.shipActions, (def) => OnShipActionSelected(def)));
				}
				//actionIcon.ShowAction(obj.actionDefintion, () => Remove(obj));
			}
		}

		private void OnShipActionSelected(ShipActionDefintion shipActionDefintion)
		{
			shipActionDefintion.OnOptionSelectedFromActionsListPanel(shipObject, OnShipActionCreated);
		}

		private void OnShipActionCreated(ShipAction shipAction)
		{
			ShipObject shipObject = shipAction.shipObject;
			shipObject.SetAction(shipAction);
		}

		/*	private void Remove(ShipAction shipAction)
			{
				shipActionList.RemoveAction(shipAction);
				Refresh();


			}
		*/

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