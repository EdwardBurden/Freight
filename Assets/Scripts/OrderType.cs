using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OrderType //type of order that can be given from player/AI
{
	Move,
	Pickup,
	PlaceDown,
	Wait,
	Attack,
	Die
}
