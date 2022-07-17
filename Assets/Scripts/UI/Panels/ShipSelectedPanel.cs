using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class ShipSelectedPanel : Panel
{
	[SerializeField]
	private TMP_Text shipNametext;

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
	private ShipActionIconButtonWidget buttonWidget; //re use for now, replace with dragbbl one later

	private ShipActionList shipActionList;

	protected override void Start()
	{
		base.Start();
		ShipManager.instance.ShipSelectedEvent += ShipManager_ShipSelectedEvent;
		ShipManager.instance.ShipDeSelectedEvent += ShipManager_ShipDeSelectedEvent1;
	}
	private void OnDestroy()
	{
		ShipManager.instance.ShipSelectedEvent -= ShipManager_ShipSelectedEvent;
		ShipManager.instance.ShipDeSelectedEvent -= ShipManager_ShipDeSelectedEvent1;
	}

	private void ShipManager_ShipSelectedEvent(ShipObject obj)
	{
		shipActionList = obj.GetComponent<ShipActionList>();
		shipActionList.ActionStartedEvent += ShipActionList_ActionStartedEvent;
		shipActionList.ActionEndedEvent += ShipActionList_ActionEndedEvent;
		shipActionList.ActionFailedEvent += ShipActionList_ActionFailedEvent;
		shipActionList.ActionPushedEvent += ShipActionList_ActionPushedEvent;

		Show();
		shipNametext.text = obj.shipName;
		Refresh();
	}

	private void Refresh()
	{
		Clear(actionListTransform);
		foreach (var item in shipActionList.activelist)
		{
			SpawnIcon(item);
		}
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

	private void SpawnIcon(ShipAction obj)
	{
		ShipActionIconButtonWidget actionIcon = Instantiate(buttonWidget, actionListTransform);
		actionIcon.ShowAction(obj.actionDefintion, null);
	}


	private void ShipActionList_ActionPushedEvent(ShipAction obj)
	{
		Refresh();
	}

	private void ShipActionList_ActionFailedEvent(ShipAction obj)
	{
		Refresh();
		//throw new System.NotImplementedException();
	}

	private void ShipActionList_ActionEndedEvent(ShipAction obj)
	{
		Refresh();
		//throw new System.NotImplementedException();
	}

	private void ShipActionList_ActionStartedEvent(ShipAction obj)
	{
		Refresh();
		//throw new System.NotImplementedException();
	}

	private void ShipManager_ShipDeSelectedEvent1(ShipObject obj)
	{
		Hide();
		shipActionList.ActionStartedEvent -= ShipActionList_ActionStartedEvent;
		shipActionList.ActionEndedEvent -= ShipActionList_ActionEndedEvent;
		shipActionList.ActionFailedEvent -= ShipActionList_ActionFailedEvent;
		shipActionList.ActionPushedEvent -= ShipActionList_ActionPushedEvent;
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
