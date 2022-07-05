namespace ActionLists
{
	public abstract class Action
	{
		public bool isFinished;
		public bool isBlocking;
		public ActionList owner;

		public Action(bool isBlocking = false)
		{
			this.isBlocking = isBlocking;
			this.isFinished = false;
		}

		public void SetOwner(ActionList actionList)
		{
			owner = actionList;
		}

		public abstract void OnUpdate();
	}
}