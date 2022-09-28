using ActionLists;
using UnityEngine;

public class AnimateLoadingPlatformCloseAction : AnimateAction
{
	LandingPlatform landingPlatform;
	public AnimateLoadingPlatformCloseAction(LandingPlatform landingPlatform) : base(landingPlatform.GetComponent<Animator>())
	{
		this.landingPlatform = landingPlatform;
	}

	public override void OnUpdate()
	{
		
	}

	public override void OnInitialise()
	{
		base.OnInitialise();
		animator.SetBool("Open", false);
		landingPlatform.CloseAnimationCompleteEvent += LandingPlatform_CloseAnimationCompleteEvent;
	}

	private void LandingPlatform_CloseAnimationCompleteEvent()
	{
		isFinished = true;
		landingPlatform.CloseAnimationCompleteEvent -= LandingPlatform_CloseAnimationCompleteEvent;
	}
}
