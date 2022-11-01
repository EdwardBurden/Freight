using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
	private void Update()
	{
		this.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward, Vector3.up);
	}
}
