using ActionLists;
using UnityEngine;

public class MoveToAction : GameObjectAction
{
	Vector3 position;
	float speed;

	public MoveToAction(Vector3 position, float speed, GameObject gameObject, bool isBlocking) : base(gameObject, isBlocking)
	{
		this.position = position;
		this.speed = speed;
	}

	public override void OnUpdate()
	{
		if (!IsAtLocation())
		{
			MoveTo();
		}
		else
		{
			isFinished = true;
		}
	}

	private bool IsAtLocation()
	{
		return Vector3.Distance(gameObject.transform.position, position) < 5;
	}

	private void MoveTo()
	{
		Vector3 direction = position - gameObject.transform.position;
		direction.Normalize();
		gameObject.transform.position += direction * Time.deltaTime * speed;
	}

}
