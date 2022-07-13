using UnityEngine;

public class AnimateLoadingPlatformCloseAction : AnimateAction
{
	public AnimateLoadingPlatformCloseAction(LandingPlatform landingPlatform) : base(landingPlatform.GetComponent<Animator>())
	{
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
