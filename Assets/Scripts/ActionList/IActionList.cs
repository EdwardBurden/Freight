using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionLists {
	public interface IActionList
	{
		public Action GetFrontAction();
	}
}