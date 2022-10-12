public interface IConsumer
{
	//Accepts - 2 or so resources it will always buy at set price 
	public ResourceDefintion[] GetAcceptedResources();
	public void SetAcceptedResources(ResourceDefintion[] resourceDefintions);
	public bool AcceptsResource(ResourceDefintion resourceData);
	//Wants - giving wants will level up building
	public ConsumerWant[] GetWants();
	public void SetWants(ConsumerWant[] consumerWants);
	//landing
	public LandingPlatform GetConsumerLandingPlatform();

}
