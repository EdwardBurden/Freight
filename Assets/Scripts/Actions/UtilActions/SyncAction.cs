using ActionLists;

public class SyncAction : Action
{
	public SyncAction() : base(true)
	{

	}

	public override void OnUpdate()
	{
		if (owner.actions[0] == this)
		{
			isFinished = true;
		}
	}
}
