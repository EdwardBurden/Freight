namespace ActionLists
{
	public abstract class Action
	{
		public bool isInitialised;
		public bool isFinished;
		public bool isBlocking;
		public ActionList owner;

		public Action(bool isBlocking = false)
		{
			this.isBlocking = isBlocking;
			this.isFinished = false;
			isInitialised = false;
		}

		public void SetOwner(ActionList actionList)
		{
			owner = actionList;
		}

		public virtual void OnInitialise()
		{
			isInitialised = true;
		}

		public abstract void OnUpdate();
	}
}