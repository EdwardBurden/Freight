using UnityEngine;

namespace ActionLists
{
	public abstract class GameObjectAction : Action
	{
		public GameObject gameObject;

		public GameObjectAction(GameObject gameObject, bool isBlocking = false) : base(isBlocking)
		{
			this.gameObject = gameObject;
		}

		protected bool IsLookingAtTarget(Vector3 target)
		{
			float angle = Vector3.Angle(GetDirection(target).normalized, gameObject.transform.forward);
			return angle < Mathf.Epsilon;
		}

		protected Vector3 GetDirection(Vector3 target)
		{
			Vector3 direction = target - gameObject.transform.position;
			return direction;
		}
	}
}