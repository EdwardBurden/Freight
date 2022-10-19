using ActionLists;
using System;

public abstract class ShipOrder
{
	public bool isFailed;
	public bool isFinished;
	public bool started;
	public ShipActionDefintion actionDefintion;
	public Ship shipObject;

	public ShipOrder(ShipActionDefintion shipActionDefintion, Ship shipObject)
	{
		actionDefintion = shipActionDefintion;
		this.shipObject = shipObject;
	}

	public ActionList childList = new ActionList();
	public string failMessage;

	public virtual void Setup()
	{
		isFailed = false;
		isFinished = false;
		started = false;
		childList.Clear();
	}

	public virtual void Start()
	{
		started = true;
	}

	public virtual void Update()
	{
		if (!isFinished && !isFailed)
		{
			childList.Update();
			isFinished = childList.IsEmpty();
		}
	}

	public virtual void Cancelled()
	{
		//use for when the order is extenrally cancelled by the user
		//communicate with buildings and unhook and events before finsihing
	}

	public virtual void Finish()
	{
		//use for when order has succesfully finished, can be used to to unhook and events or send debug messages 
	}

	protected void RaiseFailEvent(string error) //testing
	{
		isFailed = true;
		failMessage = error;
	}
}
