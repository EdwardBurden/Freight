using System.Collections.Generic;

namespace ActionLists
{
	public class ActionList
	{
		public List<Action> actions = new List<Action>();

		public void PushAction(Action action)
		{
			action.SetOwner(this);
			actions.Add(action);
		}

		public bool IsEmpty()
		{
			return actions.Count == 0;
		}

		public void Update()
		{
			if (IsEmpty())
			{
				return;
			}

			int index = 0;
			bool blocked = false;

			while (!blocked && index < actions.Count)
			{
				Action action = actions[index];
				if (!action.isInitialised)
				{
					action.OnInitialise();
				}
				action.OnUpdate();
				blocked = action.isBlocking;
				if (actions[index].isFinished)
				{
					actions.RemoveAt(index);
				}
				else
				{
					index++;
				}
			}
		}

		public void Clear()
		{
			actions.Clear();
		}
	}
}