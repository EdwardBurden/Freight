using ActionLists;

public class SyncAction : Action
{
	public SyncAction() : base(true)
	{

	}

	public override void OnUpdate()
	{
		if (owner.GetFrontAction() == this)
		{
			isFinished = true;
		}
	}
}
