using UnityEngine;

public class AnimateLoadingPlatformOpenAction : AnimateAction
{
	public AnimateLoadingPlatformOpenAction(LandingPlatform landingPlatform) : base(landingPlatform.GetComponent<Animator>())
	{
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
