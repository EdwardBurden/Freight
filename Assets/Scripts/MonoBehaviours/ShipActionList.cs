using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActionLists;


public class ShipActionList : MonoBehaviour, IActionList
{
	//move these into shipaction 
	public event System.Action<ShipAction> ActionPushedEvent;
	public event System.Action<ShipAction> ActionStartedEvent;
	public event System.Action<ShipAction> ActionFailedEvent;
	public event System.Action<ShipAction> ActionFinishedEvent;

	private Ship shipObject;
	public List<ShipAction> actions = new List<ShipAction>(); //private later

	private List<ShipAction> activelist
	{
		get
		{
			List<ShipAction> shipActions = new List<ShipAction>();
			foreach (ShipAction action in actions)
			{
				if (!action.isFinished)
				{
					shipActions.Add(action);
				}
			}
			return shipActions;
		}
	} //private later

	public bool repeat { get; set; }
	public bool paused { get; set; }

	public int maxActions; //ShipDefintion should find this component and set this

	private void Awake()
	{
		//shipObject = GetComponent<Ship>();
		//maxActions = shipObject.MaxActions;
		repeat = false;
		paused = false;
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
		//action.OnRemove();
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

	private void ResetActions()
	{
		foreach (ShipAction action in actions)
		{
			action.Reset();
		}
	}

	public int GetActionsCount()
	{
		return actions.Count;
	}

	public Action GetFrontAction()
	{
		return activelist[0];
	}
}