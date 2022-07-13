using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/ResourceData")]
public class ResourceData : ScriptableObject
{
	public Sprite icon;
	public FreightObject freightPrefab;
}
