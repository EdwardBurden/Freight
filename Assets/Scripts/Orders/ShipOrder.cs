using ActionLists;
using System;

public abstract class ShipOrder
{
	//Events
	public event Action<ShipOrder, string> OrderFailedEvent; //maybe on these

	//Data
	public bool isFinished;
	public bool started;
	//dont change
	public ShipActionDefintion actionDefintion;
	public Ship shipObject;

	public ShipOrder(ShipActionDefintion shipActionDefintion, Ship shipObject)
	{
		actionDefintion = shipActionDefintion;
		this.shipObject = shipObject;
	}

	public ActionList childList = new ActionList();

	public virtual void Setup() //called from ShipOrderQueue, set actionlist for new/repeated orders
	{
		isFinished = false;
		started = false;
	}

	public virtual void Start()//called from ShipOrderQueue, tells order to start
	{
		started = true;
	}

	public virtual void Update()//called from ShipOrderQueue ,tells order to update
	{
		if (!isFinished)
		{
			childList.Update();
			isFinished = childList.IsEmpty();
		}
	}

	public virtual void Finish()//called from ShipOrderQueue , tells order to finish up wherever it is
	{

	}

	protected void RaiseFailEvent(string error)
	{
		isFinished = true;
		OrderFailedEvent?.Invoke(this, error);
	}
}
