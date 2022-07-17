using UnityEngine;

public class AnimateLoadingPlatformOpenAction : AnimateAction
{
	LandingPlatform landingPlatform;
	public AnimateLoadingPlatformOpenAction(LandingPlatform landingPlatform) : base(landingPlatform.GetComponent<Animator>() , true)
	{
		this.landingPlatform = landingPlatform;
	}

	public override void OnUpdate()
	{
		
	}

	public override void OnInitialise()
	{
		base.OnInitialise();
		animator.SetBool("Open", true);
		landingPlatform.OpenAnimationCompleteEvent += LandingPlatform_OpenAnimationCompleteEvent;
	}

	private void LandingPlatform_OpenAnimationCompleteEvent()
	{
		isFinished = true;
		landingPlatform.OpenAnimationCompleteEvent -= LandingPlatform_OpenAnimationCompleteEvent;
	}
}
