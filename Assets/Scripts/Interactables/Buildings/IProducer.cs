using UnityEngine;

public interface IProducer
{
	public LandingPlatform GetProducerLandingPlatform();

	public ResourceDefintion[] GetProducedResources();
	public bool CanProducesResource(ResourceDefintion resourceData);

	public void SellResourceToShip(ResourceDefintion resource, Ship ship);
	public Transform GetProducerNavigationPostion();
}
