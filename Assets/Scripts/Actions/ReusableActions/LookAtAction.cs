using ActionLists;
using UnityEngine;

public class LookAtAction : GameObjectAction
{
	Vector3 target;
	float turnSpeed;

	public LookAtAction(Vector3 position, float speed, GameObject gameObject, bool isBlocking = false) : base(gameObject, isBlocking)
	{
		this.target = position;
		this.turnSpeed = speed;
	}

	public override void OnUpdate()
	{
		if (!IsLookingAtTarget(target))
		{
			LookAt();
		}
		else
		{
			isFinished = true;
		}
	}

	private void LookAt()
	{
		Quaternion look = Quaternion.LookRotation(GetDirection(target), Vector3.up);
		gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, look, turnSpeed * Time.deltaTime);
	}
}
