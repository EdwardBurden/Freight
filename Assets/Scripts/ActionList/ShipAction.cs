using UnityEngine;

namespace ActionLists
{
	public abstract class ShipAction : Action
	{
		public ShipObject shipObject;

		public ShipAction(ShipObject shipObject, bool isBlocking = false) : base(isBlocking)
		{
			this.shipObject = shipObject;
		}

		protected bool IsLookingAtTarget(Vector3 target) //TODO move to util
		{
			float angle = Vector3.Angle(GetDirection(target).normalized, shipObject.gameObject.transform.forward);
			return angle < Mathf.Epsilon;
		}

		protected Vector3 GetDirection(Vector3 target) //TODO move to util
		{
			Vector3 direction = target - shipObject.gameObject.transform.position;
			return direction;
		}
	}
}