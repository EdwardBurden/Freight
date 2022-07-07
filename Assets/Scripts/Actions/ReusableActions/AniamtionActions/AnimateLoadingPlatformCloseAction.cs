using ActionLists;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateLoadingPlatformCloseAction : AnimateAction
{
	private LandingPlatform landingPlatform;

	public AnimateLoadingPlatformCloseAction(LandingPlatform landingPlatform) : base(landingPlatform.GetComponent<Animator>())
	{
		this.landingPlatform = landingPlatform;

	}


	public override void OnUpdate()
	{
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("Close") && !AnimatorIsPlaying())
		{
			isFinished = true;
		}
	}

	public override void OnInitialise()
	{
		base.OnInitialise();
		animator.SetBool("Open", false);
	}
}
