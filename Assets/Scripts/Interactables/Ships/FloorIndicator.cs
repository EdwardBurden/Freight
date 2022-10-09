using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorIndicator : MonoBehaviour
{

	public GameObject floor;
	public LineRenderer floorToShipLine;


	private void Update()
	{
		RayCastInfo<Surface> rayCastInfo = RayCastUtils.FindObjectHit<Surface>(this.transform.position + Vector3.up, Vector3.down);
		if (rayCastInfo.focused)
		{
			floor.transform.position = rayCastInfo.hitInfo.point;
			floor.transform.eulerAngles = new Vector3(0, this.transform.rotation.eulerAngles.y, 0);
			floorToShipLine.SetPosition(0, Vector3.zero);
			floorToShipLine.SetPosition(1, floor.transform.localPosition);
		}
	}

}
