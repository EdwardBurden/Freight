using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActionLists;
using System.Linq;
//TODO
public class ShipActionList : MonoBehaviour, IActionList
{
	public event System.Action<ShipAction> ActionPushedEvent;
	public event System.Action<ShipAction> ActionStartedEvent;
	public event System.Action<ShipAction> ActionFailedEvent;
	public event System.Action<ShipAction> ActionFinishedEvent;

	public List<ShipAction> actions = new List<ShipAction>();
	//use actions for ui, grey out when isfinished .When all finsihed reset list
	public List<ShipAction> activelist => actions.Where(x => !x.isFinished).ToList(); //TODO remove linq

	public bool repeat { get; private set; }
	public bool paused { get; private set; }

	public int maxActions = 3;

	private void Awake()
	{
		repeat = false;
		paused = false;
	}

	public void ClearActions()
	{
		actions.Clear(); //change later 
	}

	public void PushAction(ShipAction action)
	{
		action.SetOwner(this);
		actions.Add(action);
		ActionPushedEvent?.Invoke(action);
	}

	public void RemoveAction(ShipAction action)
	{
		action.OnRemove();
		actions.Remove(action);
	}

	private void FinishAction(ShipAction shipAction)
	{
		shipAction.OnFinish();
		if (!repeat)
		{
			actions.Remove(shipAction);
		}
		ActionFinishedEvent?.Invoke(shipAction);
	}

	private void InitialiseAction(ShipAction shipAction)
	{
		shipAction.OnInitialise();
		ActionStartedEvent?.Invoke(shipAction);
	}

	private void ActionFailed(ShipAction shipAction)
	{
		paused = true;
		repeat = false;
		ResetActions();
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
			if (repeat && actions.Count > 1)
			{
				ResetActions();
			}
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

	private void ResetActions()
	{
		foreach (var item in actions)
		{
			item.Reset();
			item.isFinished = false;
			item.isInitialised = false;
			item.failed = false;
		}

	}

	public int GetActionsPushedAmount()
	{
		return actions.Count;
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