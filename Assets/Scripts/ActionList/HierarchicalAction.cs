namespace ActionLists
{
	public abstract class HierarchicalAction : Action
	{
		public ActionList actionList = new ActionList();

		public HierarchicalAction() : base(true)
		{

		}

		public override void OnUpdate()
		{
			actionList.Update();
			isFinished = actionList.IsEmpty();
		}
	}
}