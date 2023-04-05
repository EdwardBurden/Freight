using System.Collections;
using System.Collections.Generic;
using UI;
using Unity.VisualScripting;
using UnityEngine;

public class RotateAndScaleToCamera : MonoBehaviour
{
	private const float MAX_CAMERA_DISTANCE = 50;

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

