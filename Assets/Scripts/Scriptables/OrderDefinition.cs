using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OrderDefinition<T> : ScriptableObject where T : Order
{
	public string orderName;
	public string orderDescription;
}
