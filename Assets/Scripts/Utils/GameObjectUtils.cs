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

	public static Vector2 xz(this Vector3 vv)
	{
		return new Vector2(vv.x, vv.z);
	}

	public static float FlatDistanceTo(this Vector3 from, Vector3 unto)
	{
		Vector2 a = from.xz();
		Vector2 b = unto.xz();
		return Vector2.Distance(a, b);
	}
}
