using ActionLists;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateLoadingPlatformOpenAction : AnimateAction
{
	private LandingPlatform landingPlatform;

	public AnimateLoadingPlatformOpenAction(LandingPlatform landingPlatform) : base(landingPlatform.GetComponent<Animator>())
	{
		this.landingPlatform = landingPlatform;

	}


	public override void OnUpdate()
	{
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("Open") && !AnimatorIsPlaying())
		{
			isFinished = true;
		}
	}

	public override void OnInitialise()
	{
		base.OnInitialise();
		animator.SetBool("Open", true);
	}
}
