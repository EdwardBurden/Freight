using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActionLists;

public class ActionListTest : MonoBehaviour
{


	public Transform[] transforms;
	public Farm farm;
	public Farm farm2;
	public Town town;
	public Town town2;

	public ShipObject shiptest;

	ActionList actionList;
	private void Start()
	{
		actionList = new ActionList();

		CollectAction collectAction = new CollectAction(shiptest, farm, farm.GetProducedResources()[0]);
		actionList.PushAction(collectAction);

		CollectAction collect2Action = new CollectAction(shiptest, farm2, farm2.GetProducedResources()[0]);
		actionList.PushAction(collect2Action);

		DeliverAction deliverAction = new DeliverAction(shiptest, town, farm.GetProducedResources()[0]);
		actionList.PushAction(deliverAction);

		DeliverAction deliver2Action = new DeliverAction(shiptest, town2, farm2.GetProducedResources()[0]);
		actionList.PushAction(deliver2Action);

		foreach (Transform transform in transforms)
		{
			MoveAction moveAction = new MoveAction(shiptest, transform.position);
			actionList.PushAction(moveAction);
		}

	}

	private void Update()
	{
		actionList.Update();
	}
}