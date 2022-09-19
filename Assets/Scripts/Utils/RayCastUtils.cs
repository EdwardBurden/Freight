using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public struct RayCastInfo<T> where T : MonoBehaviour //to move
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


public static class RayCastUtils 
{
	public static RayCastInfo<T> FindFirstObjectHit<T>(Vector3 startpostion, Vector3 direction, float distance = 100) where T : MonoBehaviour
	{
		RaycastHit hit = new RaycastHit();
		bool inSight = false;
		T foundObject = null;
		Ray ray = new Ray(startpostion, direction);
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

	public static RayCastInfo<T> CheckIfObjectIsInFocus<T>(Camera camera) where T : MonoBehaviour
	{
		RaycastHit hit = new RaycastHit();
		bool inSight = false;
		T foundObject = null;
		Ray ray = camera.ScreenPointToRay(Input.mousePosition);
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

	public static bool CheckIfObjectIsUIInFocus(Vector3 mousePosition)
	{
		PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
		pointerEventData.position = mousePosition;

		List<RaycastResult> raycastResults = new List<RaycastResult>();
		EventSystem.current.RaycastAll(pointerEventData, raycastResults);
		for (int i = 0; i < raycastResults.Count; i++)
		{
			if (raycastResults[i].gameObject.layer == 5)
			{
				return true;
			}
		}
		return false;
	}
}
