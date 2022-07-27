using ActionLists;
using UnityEngine;

namespace ActionLists
{
	public class DelayAction : Action
	{
		public float time;
		private float timer;

		public DelayAction(int time) : base(true)
		{
			this.time = time;
			timer = 0;
		}

		public override void OnUpdate()
		{
			timer += Time.deltaTime;
			if (timer > time)
			{
				isFinished = true;
			}
		}
	}
}