public interface IProducer
{
	public ResourceDefintion[] GetProducedResources();
	public LandingPlatform GetProducerLandingPlatform();
	public bool ProducesResource(ResourceDefintion resourceData);
}
