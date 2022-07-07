using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActionLists;

public class ActionListTest : MonoBehaviour
{
	public Transform[] transforms;
	public Farm farm;
	public Town town;

	public CarryObject shiptest;

	ActionList actionList;
	private void Start()
	{
		actionList = new ActionList();

		CollectAction collectAction = new CollectAction(shiptest, farm, farm.GetProducedResources()[0]);
		actionList.PushAction(collectAction);

		DeliverAction deliverAction = new DeliverAction(shiptest.gameObject, town, farm.GetProducedResources()[0]);
		actionList.PushAction(deliverAction);


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