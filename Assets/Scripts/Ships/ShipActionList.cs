using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActionLists;
//TODO
public class ShipActionList : MonoBehaviour, IActionList
{
	public event System.Action<ShipAction> ActionPushedEvent;
	public event System.Action<ShipAction> ActionStartedEvent;
	public event System.Action<ShipAction> ActionFailedEvent;
	public event System.Action<ShipAction> ActionEndedEvent;

	public List<ShipAction> storedList = new List<ShipAction>();
	public List<ShipAction> activelist = new List<ShipAction>();
	public bool repeat { get; private set; }
	public bool paused { get; private set; }

	public int maxActions = 3;

	private void Awake()
	{
		repeat = false;
		paused = false;
	}

	public void PushAction(ShipAction action)
	{
		action.SetOwner(this);
		activelist.Add(action);
		ActionPushedEvent?.Invoke(action);
	}
	private void FinishAction(ShipAction shipAction)
	{
		shipAction.OnFinish();
		activelist.Remove(shipAction);
		ActionEndedEvent?.Invoke(shipAction);
	}

	private void InitialiseAction(ShipAction shipAction)
	{
		shipAction.OnInitialise();
		ActionStartedEvent?.Invoke(shipAction);
	}

	private void ActionFailed(ShipAction shipAction)
	{
		activelist.Remove(shipAction);
		ActionFailedEvent?.Invoke(shipAction);
	}

	private bool IsEmpty()
	{
		return activelist.Count == 0;
	}

	public void Update()
	{
		if (paused)
		{
			return;
		}

		if (IsEmpty())
		{
			return;
		}

		int index = 0;
		bool blocked = false;

		while (!blocked && index < activelist.Count)
		{
			ShipAction action = activelist[index];
			if (!action.isInitialised)
			{
				InitialiseAction(action);
			}
			action.OnUpdate();
			blocked = action.isBlocking;
			if (action.isFinished)
			{
				if (action.failed)
				{
					ActionFailed(action);
				}
				else
				{
					FinishAction(action);
				}
			}
			else
			{
				index++;
			}
		}
	}

	internal void SetPaused(bool v)
	{
		paused = v;
		//trigger event
	}

	public Action GetFrontAction()
	{
		return activelist[0];
	}

	internal void SetRepeat(bool v)
	{
		repeat = v;
		//trigger event
	}
}