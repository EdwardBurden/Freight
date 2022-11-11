using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UI
{
	public abstract class InCamera : MonoBehaviour
	{
		private const float MAX_CAMERA_DISTANCE = 50;

		//TODO , remove this class, move follow function into seperate component
		protected Transform follow;
		protected RectTransform rectTransform;
		protected Vector3 startScale;

		private void Awake()
		{
			startScale = this.transform.localScale;
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
			this.transform.localScale = startScale / Mathf.Max(1, (Vector3.Distance(follow.position, Camera.main.transform.position) - MAX_CAMERA_DISTANCE) * 0.1f);
		}
	}
}