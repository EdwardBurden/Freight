using UnityEngine;

namespace ActionLists
{
	public abstract class AnimateAction : Action
	{
		protected Animator animator;

		public AnimateAction(Animator animator) : base(true)
		{
			this.animator = animator;
		}
	}
}