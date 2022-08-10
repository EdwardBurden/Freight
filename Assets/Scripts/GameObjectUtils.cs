using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectUtils
{
	public static bool IsLookingAtTarget(this GameObject gameObject, Vector3 target)
	{
		Vector3 direction = GetDirection(gameObject, target).normalized;
		float angle = Vector3.Angle(direction, gameObject.transform.forward);
		return angle < Mathf.Epsilon;
	}

	public static Vector3 GetDirection(this GameObject gameObject, Vector3 target)
	{
		Vector3 direction = target - gameObject.transform.position;
		return direction;
	}

	public static void Clear(Transform transform)
	{
		foreach (Transform item in transform)
		{
			GameObject.Destroy(item.gameObject);
		}
	}
}
