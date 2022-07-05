using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActionLists;

public class ActionListTest : MonoBehaviour
{
	public Transform[] transforms;

	public ShipTest shiptest;

	ActionList actionList;
	private void Start()
	{
		actionList = new ActionList();
		foreach (Transform transform in transforms)
		{
			MoveAction moveAction = new MoveAction(shiptest.gameObject, transform.position);
			actionList.PushAction(moveAction);
		}
	}

	private void Update()
	{
		actionList.Update();
	}
}