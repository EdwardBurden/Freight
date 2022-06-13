using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
	public Transform look;

	public float maxRotation = 10; //in degrees
								   //public float minRotation = 10; //in degrees

	public GameObject[] engines;

	[Range(0, 100)]
	public float speed;



	private void Update()
	{
		//AnimateEngines(leftToRight);
		Vector3 targetDirection = look.transform.position - this.transform.position;
		Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
		this.transform.localRotation = Quaternion.RotateTowards(this.transform.rotation, targetRotation, speed * Time.deltaTime);

		float dot = Vector3.Dot(this.transform.right, targetDirection.normalized);
		Debug.LogWarning(dot);
		float rotation = maxRotation * dot;


		foreach (GameObject item in engines)
		{
			item.transform.localRotation = Quaternion.Euler(speed, rotation, 0);
		}
	}



}
