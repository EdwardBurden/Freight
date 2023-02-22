using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumeRequest : ResourceRequest
{

	//TODO keep track f storage in here andhave building use to verify it can level  up 
	public ConsumeRequest(ResourceDefintion resource, int amount) : base(resource, amount)
	{
	}
}
