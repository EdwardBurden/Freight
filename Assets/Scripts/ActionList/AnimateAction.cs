using ActionLists;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimateAction : Action
{
	protected Animator animator;

	public AnimateAction(Animator animator) : base()
	{
		this.animator = animator;
	}

	protected bool AnimatorIsPlaying()
	{
		return animator.GetCurrentAnimatorStateInfo(0).length >
			   animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
	}

	protected bool AnimatorIsPlaying(string stateName)
	{
		return AnimatorIsPlaying() && animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
	}

	protected bool isPlaying( string stateName)
	{
		if (animator.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
				animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
			return true;
		else
			return false;
	}
}
