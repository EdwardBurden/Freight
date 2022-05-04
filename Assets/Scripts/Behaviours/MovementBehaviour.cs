using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{
	public Transform aniamtionRoot;
	public float speed;
	public float turnSpeed;

	private const string MOVING_ANIAMTION_BOOL = "Speed";
	private const float MINDISTANCE = 0.01f;
	private Vector3? target = null; //could add desired transform too
	private Animator animator;

	public event Action MovementComplete;// Tracked by queue system and any other system(sound, notification), could be put into event system in future
										 //if problem, put code here to tell the world, then in moveorder listen to event
	private void Update()
	{
		if (target == null)
		{
			return;
		}
		if (!IsAt(target.Value))
		{
			MoveTo(target.Value);
			return;
		}
		ReachedTarget();
	}

	public void StartMovement(Vector3 target)
	{
		this.target = target;
		animator = gameObject.GetComponent<Animator>();
		if (IsAt(target))
		{
			ReachedTarget();
		}
	}

	private void ReachedTarget()
	{
		target = null;
		animator.SetFloat(MOVING_ANIAMTION_BOOL, 0);
		MovementComplete?.Invoke();
	}

	private bool IsAt(Vector3 target)
	{
		return Vector3.Distance(target, this.transform.position) <= MINDISTANCE;
	}

	private void MoveTo(Vector3 target) //simple
	{
		animator.SetFloat(MOVING_ANIAMTION_BOOL, 1);

		Vector3 direction = target - this.transform.position;
		direction.Normalize();
		this.transform.position += direction * Time.deltaTime * speed;

		Quaternion look = Quaternion.LookRotation(direction, Vector3.up);
		this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, look, turnSpeed * Time.deltaTime);
	}
}
