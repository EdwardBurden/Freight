using ActionLists;
using UnityEngine;

namespace ActionLists
{
	public abstract class AnimateAction : Action
	{
		protected Animator animator;

		public AnimateAction(Animator animator, bool isBlocking = false) : base(isBlocking)
		{
			this.animator = animator;
		}
	}
}