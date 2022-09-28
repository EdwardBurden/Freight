public interface IConsumer
{
	public ResourceDefintion[] GetAcceptedResources();
	LandingPlatform GetConsumerLandingPlatform();
	bool AcceptsResource(ResourceDefintion resourceData);
	int GetBuyPrice(ResourceDefintion resource);
}
