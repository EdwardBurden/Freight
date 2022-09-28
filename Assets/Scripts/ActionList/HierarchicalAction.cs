namespace ActionLists
{
	public abstract class HierarchicalAction : Action
	{
		public ActionList childList = new ActionList();

		public HierarchicalAction() : base(true)
		{
		}

		public override void OnUpdate()
		{
			childList.Update();
			isFinished = childList.IsEmpty();
		}
	}
}