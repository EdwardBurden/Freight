using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class CustomBehaviour : MonoBehaviour
{
	protected Animator animator;
	protected virtual void Awake()
	{
		animator = gameObject.GetComponent<Animator>();
	}

	public abstract void StopBehaviour();


	protected void RotateToRotation(Quaternion quaternion, float turnSpeed) //can be moved to helpers
	{
		this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, quaternion, turnSpeed * Time.deltaTime);
	}

	protected bool IsMatchingRotation(Quaternion quaternion, float minRotatiom) //can be moved to helpers
	{
		return Quaternion.Angle(this.transform.rotation, quaternion) < minRotatiom;
	}

	protected bool IsAt(Vector3 target, float minDistance)
	{
		return Vector3.Distance(target, this.transform.position) <= minDistance;
	}
}
