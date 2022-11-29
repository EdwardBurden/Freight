public class ResourceRequest
{
	public int amountRequired;
	public ResourceDefintion resourceDefintion;

	public ResourceRequest(int amountRequired, ResourceDefintion resourceDefintion)
	{
		this.amountRequired = amountRequired;
		this.resourceDefintion = resourceDefintion;
	}
}