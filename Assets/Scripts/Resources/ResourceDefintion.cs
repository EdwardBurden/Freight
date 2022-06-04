using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Resources/ResourceDefintion", order = 1)]
public class ResourceDefintion : ScriptableObject
{
	public string resourceName;
	public string resourceDescription;
	public Sprite icon;
}
