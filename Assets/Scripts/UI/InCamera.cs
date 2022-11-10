using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UI
{
	public abstract class InCamera : MonoBehaviour
	{
		//TODO , remove this class, move follow function into seperate component
		protected Transform follow;
		protected RectTransform rectTransform;

		private void Awake()
		{
			rectTransform = GetComponent<RectTransform>();
		}

		public virtual void SetFollow(Transform transform)
		{
			follow = transform;
			Follow();
		}

		private void Update()
		{
			if (follow != null)
			{
				Follow();
			}
		}

		protected virtual void Follow()
		{
			rectTransform.anchoredPosition = GameUIController.instance.GetInScenePosition(follow.position);
		}
	}
}