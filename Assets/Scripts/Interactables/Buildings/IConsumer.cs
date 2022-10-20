public interface IConsumer
{
	//Accepts - 2 or so resources it will always buy at set price 
	public ResourceDefintion[] GetAcceptedResources();
	public void SetAcceptedResources(ResourceDefintion[] resourceDefintions);
	public bool AcceptsResource(ResourceDefintion resourceData);
	//landing
	public LandingPlatform GetConsumerLandingPlatform();

	public void BuyResourceFromShip(ResourceDefintion resource, Ship ship);
}
