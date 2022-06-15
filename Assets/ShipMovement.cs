using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
	public Transform look;

	public float maxXRotation = 10; //in degrees
	public float maxYRotation = 10; //in degrees

	public float maxSpeed = 10;
	public float acc = 0.5f;

	public GameObject[] engines;

	public float moveSpeed;
	public float turnSpeed;

	private Vector3 targetDirection => look.position - this.transform.position;

	private void Start()
	{
		StartCoroutine(Movement());
	}

	private IEnumerator Movement()
	{
		while (true)
		{
			bool atTraget = IsAtTarget();

			if (!atTraget)
			{
				if (!IsLookingAtTarget())
				{
					Rotate();
					moveSpeed -= acc;
					moveSpeed = Mathf.Max(0, moveSpeed);
				}
				Move();
			}
			else
			{
				moveSpeed -= acc;
				moveSpeed = Mathf.Max(0, moveSpeed);
			}

			yield return null;
		}
	}


	private void Update()
	{
		float dot = Vector3.Dot(this.transform.right, targetDirection.normalized);
		float xRotation = maxXRotation * dot;
		float yRotation = maxYRotation * (moveSpeed / maxSpeed);
		foreach (GameObject item in engines)
		{
			item.transform.localRotation = Quaternion.Euler(yRotation, xRotation, 0);
		}
	}

	private bool IsAtTarget()
	{
		return Vector3.Distance(this.transform.position, look.position) < 0.005f;
	}

	protected bool IsLookingAtTarget() //can be moved to helpers
	{
		return Vector3.Angle(this.transform.forward.normalized, targetDirection.normalized) < 0.005f;
	}


	public void Move()
	{
		moveSpeed += acc;
		moveSpeed = Mathf.Min(moveSpeed, maxSpeed);
		transform.position = Vector3.MoveTowards(transform.position, look.position, moveSpeed * Time.deltaTime);
	}

	public void Rotate()
	{
		Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
		this.transform.localRotation = Quaternion.RotateTowards(this.transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
	}
}
