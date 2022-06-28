using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBillboard : MonoBehaviour
{
	private Camera camera;

	private Vector3 up;
	private void Awake()
	{
		camera = Camera.main;//TODO can change later if needed
	}

	private void LateUpdate()
	{
		Vector3 targetPostition = new Vector3(this.transform.position.x,
											camera.transform.position.y,
											camera.transform.position.z);
		this.transform.LookAt(targetPostition);
	}
}
