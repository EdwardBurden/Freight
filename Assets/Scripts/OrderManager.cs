using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
	public GameObject target;

	public Orderable activeOrderable;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.T))
		{
			Test();
		}
		if (Input.GetKeyDown(KeyCode.P))
		{
			//PauseOrder();
		}
		if (Input.GetKeyDown(KeyCode.C))
		{
			//	PauseOrder();
		}

	}






	void Test()
	{
		MoveOrder moveOrder = new MoveOrder(new Vector3(10, 1, 1), activeOrderable);
		activeOrderable.AddOrder(moveOrder);
		PickUpOrder pickup = new PickUpOrder(target, activeOrderable);
		activeOrderable.AddOrder(pickup);

		activeOrderable.AddOrder(moveOrder);

		//	WaitOrder waitOrder = new WaitOrder(2, this);
		//	SendOrder(waitOrder);



		//	moveOrder = new MoveOrder(new Vector3(10, 1, 1), this);
		//	SendOrder(moveOrder);

		activeOrderable.StartOrders();
	}
}
