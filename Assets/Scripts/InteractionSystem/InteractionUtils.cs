using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InteractionUtils
{
	public struct RayCastInfo<T> where T : MonoBehaviour
	{
		public bool focused; //True if Monobehaviour looking for exists in raycasts
		public T focusObject; //The Monobehaviour looked for, closest first
		public RaycastHit hitInfo;
		public RayCastInfo(bool focus, RaycastHit info, T focusobject)
		{
			focused = focus;
			hitInfo = info;
			focusObject = focusobject;
		}
	}

	public static RayCastInfo<T> CheckIfObjectIsInFocus<T>() where T : MonoBehaviour
	{
		RaycastHit hit = new RaycastHit();
		bool inSight = false;
		T foundObject = null;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float distance = 10000;
		RaycastHit[] raycasts = Physics.RaycastAll(ray);
		for (int i = 0; i < raycasts.Length; i++)
		{
			if (raycasts[i].distance < distance)
			{
				distance = raycasts[i].distance;
				T foundobject = raycasts[i].collider.gameObject.GetComponent<T>();
				if (foundobject)
				{
					inSight = true;
					hit = raycasts[i];
					foundObject = foundobject;
				}
				else
					inSight = false;
			}
		}
		return new RayCastInfo<T>(inSight, hit, foundObject);
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