using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveBehaviour : CustomBehaviour
{
	public AnimationCurve movementCurve;
	private const string MOVING_ANIAMTION_BOOL = "Speed";
	private const float MINDISTANCE = 0.01f;

	public Transform aniamtionRoot;

	public float maxSpeed; //or goto ship behaviour, get speed for defintion 
	public float turnSpeed; //or goto ship behaviour, get speed for defintion 
	public float accelerationAmount;
	public float acc = 0.05f;
	private Vector3? target = null; //could add desired transform too
	private Animator animator;

	public event Action movementComplete;// Tracked by queue system and any other system(sound, notification), could be put into event system in future

	private void Awake()
	{
		animator = gameObject.GetComponent<Animator>();
	}

	private void Update()
	{
		if (target != null)
		{
			if (!IsAt(target.Value))
			{
				MoveTo(target.Value);
				return;
			}
			ReachedTarget();
			return;
		}

		accelerationAmount -= acc * Time.deltaTime;
		accelerationAmount = Math.Max(accelerationAmount, 0);
	}

	public void StartMovement(Vector3 target)
	{
		//this.target = new Vector3(target.x, 0, target.z); //for now only x,z
		this.target = target;
		if (IsAt(target))
		{
			ReachedTarget();
		}
	}

	public override void StopBehaviour()
	{
		//accelerationAmount = 0;
		target = null;
		animator.SetFloat(MOVING_ANIAMTION_BOOL, 0);
	}

	private void ReachedTarget()
	{
		StopBehaviour();
		movementComplete?.Invoke();
	}

	private bool IsAt(Vector3 target)
	{
		return Vector3.Distance(target, this.transform.position) <= MINDISTANCE;
	}

	private void MoveTo(Vector3 target) //simple
	{
		//animator.SetFloat(MOVING_ANIAMTION_BOOL, 1);

		Vector3 direction = target - this.transform.position;
		direction.Normalize();
		accelerationAmount += Time.deltaTime * acc;
		accelerationAmount = Math.Min(accelerationAmount, 1);
		float speedModifier = movementCurve.Evaluate(accelerationAmount);
		this.transform.position += direction * Time.deltaTime * (speedModifier * maxSpeed);

		Quaternion look = Quaternion.LookRotation(direction, Vector3.up);
		this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, look, (turnSpeed * speedModifier) * Time.deltaTime);
		animator.SetFloat(MOVING_ANIAMTION_BOOL, speedModifier);
	}
}
