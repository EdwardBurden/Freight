public interface IProducer
{
	public ResourceDefintion[] GetProducedResources();
	LandingPlatform GetProducerLandingPlatform();
	bool ProducesResource(ResourceDefintion resourceData);
	int GetSellPrice(ResourceDefintion resource);
}
