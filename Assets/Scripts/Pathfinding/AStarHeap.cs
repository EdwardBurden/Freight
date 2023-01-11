using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

namespace AStar
{
	public class AStarHeap<T> where T : IHeapItem<T>
	{
		private T[] items;
		private int currentItemCount;

		public AStarHeap(int maxHeapSize)
		{
			items = new T[maxHeapSize];

		}

		public void AddItem(T item)
		{
			item.HeapIndex = currentItemCount;
			items[currentItemCount] = item;
			SortUp(item);
			currentItemCount++;
		}


		public T RemoveFirst()
		{
			T item = items[0];
			currentItemCount--;
			items[0] = items[currentItemCount];
			items[0].HeapIndex = 0;
			SortDown(items[0]);
			return item;
		}

		public void UpdateItem(T item)
		{
			SortUp(item);
		}

		public bool Contains(T item)
		{
			return Equals(items[item.HeapIndex], item);
		}

		public int Count()
		{
			return currentItemCount;
		}

		private void SortDown(T item)
		{
			while (true)
			{
				int child0 = item.HeapIndex * 2 + 1;
				int child1 = item.HeapIndex * 2 + 2;
				int swapIndex = 0;

				if (child0 < currentItemCount)
				{
					swapIndex = child0;

					if (child1 < currentItemCount)
					{
						if (items[child0].CompareTo(items[child1]) < 0)
						{
							swapIndex = child1;
						}
					}

					if (item.CompareTo(items[swapIndex]) < 0)
					{
						Swap(item, items[swapIndex]);
					}
					else
					{
						return;
					}
				}
				else
				{
					return;
				}
			}
		}

		private void SortUp(T item)
		{
			int parentIndex = (item.HeapIndex - 1) / 2;

			while (true)
			{
				T parent = items[parentIndex];
				if (item.CompareTo(parent) > 0)
				{
					Swap(item, parent);
				}
				else
				{
					break;
				}
				parentIndex = (item.HeapIndex - 1) / 2;
			}
		}

		private void Swap(T item1, T item2)
		{
			items[item1.HeapIndex] = item2;
			items[item2.HeapIndex] = item1;

			int index = item1.HeapIndex;
			item1.HeapIndex = item2.HeapIndex;
			item2.HeapIndex = index;
		}
	}

}

