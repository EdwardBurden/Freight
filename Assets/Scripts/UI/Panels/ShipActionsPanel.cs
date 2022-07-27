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

		private ShipActionList shipActionList;

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
				ShipAction shipAction = null;
				if (i < shipActionList.actions.Count)
				{
					shipAction = shipActionList.actions[i];
				}
				ShipActionsPanelActionButtonWidget actionIcon = Instantiate(buttonWidget, actionListTransform);
				actionIcon.Show(shipAction);
				//actionIcon.ShowAction(obj.actionDefintion, () => Remove(obj));
			}
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
			//throw new System.NotImplementedException();
		}

		private void OnActionFinished(ShipAction obj)
		{
			Refresh();
			//throw new System.NotImplementedException();
		}

		private void OnActionStarted(ShipAction obj)
		{
			Refresh();
			//throw new System.NotImplementedException();
		}
		private void OnShipSelected(ShipObject obj)
		{
			shipActionList = obj.GetComponent<ShipActionList>();
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
			//register events from shipobject
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