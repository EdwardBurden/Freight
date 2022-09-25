using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UI
{
	public class InScene : MonoBehaviour
	{
		protected Transform follow;
		[SerializeField]
		protected RectTransform uiTransform;

		public virtual void SetFollow(Transform transform)
		{
			follow = transform;
			SetAnchor();
		}

		private void Update()
		{
			if (follow != null)
			{
				SetAnchor();
			}
		}

		private void SetAnchor()
		{
			uiTransform.anchoredPosition = UIUtils.GetAnchoredPostionFromWorldSpacePoint(follow.position, UIManager.instance.inScene);
		}
	}
}