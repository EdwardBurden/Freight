using ActionLists;
using System;

public abstract class ShipOrder
{
	public event Action<ShipOrder, string> OrderFailedEvent;

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
	internal string failMessage;

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

	public virtual void Finish()
	{
	}

	protected void RaiseFailEvent(string error) //testing
	{
		isFailed = true;
		failMessage = error;
		//OrderFailedEvent?.Invoke(this, error);
	}
}
