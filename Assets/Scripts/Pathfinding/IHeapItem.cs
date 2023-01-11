using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStar
{
	public interface IHeapItem<T> : IComparable<T>
	{
		int HeapIndex { get; set; }
	}
}
