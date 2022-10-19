public interface IConsumer
{
	//Accepts - 2 or so resources it will always buy at set price 
	public ResourceDefintion[] GetAcceptedResources();
	public void SetAcceptedResources(ResourceDefintion[] resourceDefintions);
	public bool AcceptsResource(ResourceDefintion resourceData);
	//Wants - giving wants will level up building
	public SettlementWants GetWants(); //move to settlement
	public void SetWants(SettlementWants consumerWants); //move to settlement
													   //landing
	public LandingPlatform GetConsumerLandingPlatform();


	public void BuyResourceFromShip(ResourceDefintion resource, Ship ship);
}
