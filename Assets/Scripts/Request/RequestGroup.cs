using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class RequestGroup
{
	public ResourceRequest[] resourceRequests;

	public RequestGroup(ResourceRequest[] resourceRequests)
	{
		this.resourceRequests = resourceRequests;
	}

	public RequestGroup(ResourceRequestDefintion[] resourceRequestDefintions)
	{
		this.resourceRequests = CreateRequest(resourceRequestDefintions);
	}

	private ResourceRequest[] CreateRequest(ResourceRequestDefintion[] resourceRequestDefintions)
	{
		List<ResourceRequest> resourceRequests = new List<ResourceRequest>();
		foreach (ResourceRequestDefintion requestDefintion in resourceRequestDefintions)
		{
			ResourceRequest resourceRequest = requestDefintion.MakeRequest();
			resourceRequests.Add(resourceRequest);
		}
		return resourceRequests.ToArray();
	}

	public ResourceDefintion[] GetResourcesFromRequests<T>() where T : ResourceRequest
	{
		List<ResourceDefintion> resourceDefintions = new List<ResourceDefintion>();
		foreach (ResourceRequest request in resourceRequests)
		{
			if (request is T)
			{
				resourceDefintions.Add(request.resource);
			}
		}
		return resourceDefintions.ToArray();

	}

	public void UpdateResourceRequests<T>(ResourceDefintion resource, int amount)  where T : ResourceRequest
	{
		foreach (ResourceRequest request in resourceRequests)
		{
			if (request is T req)
			{
				req.UpdateRequest(amount);
			}
		}
	}
}

