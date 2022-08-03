using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/ConversionData")]
public class ConversionDefintion : ScriptableObject
{
    public ConversionInput[] inputs;
    public ResourceDefintion[] outputs;
}
